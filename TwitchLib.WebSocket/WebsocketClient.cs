using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.WebSocket.Enums;
using TwitchLib.WebSocket.Events;

namespace TwitchLib.WebSocket
{
    public class WebSocketClient : IWebsocketClient, IDisposable
    {
        private string Url { get; }
        private ClientWebSocket _ws;
        private readonly IWebsocketClientOptions _options;
        private readonly BlockingCollection<Tuple<DateTime, string>> _sendQueue = new BlockingCollection<Tuple<DateTime, string>>();
        private bool _disconnectCalled;
        private bool _listenerRunning;
        private bool _senderRunning;
        private bool _monitorRunning;
        private bool _reconnecting;
        private bool _resetThrottlerRunning;
        private int _sentCount = 0;
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private Task _monitor;
        private Task _listener;
        private Task _sender;
        private Task _resetThrottler;

        /// <summary>
        /// The current state of the connection.
        /// </summary>
        public WebSocketState State => _ws.State;

        /// <summary>
        /// The current number of items waiting to be sent.
        /// </summary>
        public int SendQueueLength => _sendQueue.Count;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public TimeSpan DefaultKeepAliveInterval
        {
            get => _ws.Options.KeepAliveInterval;
            set => _ws.Options.KeepAliveInterval = value;
        }

        #region Events
        /// <summary>
        /// Fires when Data (ByteArray) is received.
        /// </summary>
        public event EventHandler<OnDataEventArgs> OnData;

        /// <summary>
        /// Fires when a Message/ group of messages is received.
        /// </summary>
        public event EventHandler<OnMessageEventArgs> OnMessage;

        /// <summary>
        /// Fires when the websocket state changes
        /// </summary>
        public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

        /// <summary>
        /// Fires when the Client has connected
        /// </summary>
        public event EventHandler<OnConnectedEventArgs> OnConnected;

        /// <summary>
        /// Fires when the Client disconnects
        /// </summary>
        public event EventHandler<OnDisconnectedEventArgs> OnDisconnected;

        /// <summary>
        /// Fires when An Exception Occurs in the client
        /// </summary>
        public event EventHandler <OnErrorEventArgs> OnError;

        /// <summary>
        /// Fires when a message Send event failed.
        /// </summary>
        public event EventHandler <OnSendFailedEventArgs> OnSendFailed;

        /// <summary>
        /// Fires when a Fatal Error Occurs.
        /// </summary>
        public event EventHandler<OnFatalErrorEventArgs> OnFatality;

        /// <summary>
        /// Fires when a Message has been throttled.
        /// </summary>
        public event EventHandler<OnMessageThrottledEventArgs> OnMessageThrottled;
        #endregion

        public WebSocketClient(IWebsocketClientOptions options)
        {
            _options = options;

            switch (options.ClientType)
            {
                case ClientType.Chat:
                    Url = options.UseWSS ? "wss://irc-ws.chat.twitch.tv:443" : "ws://irc-ws.chat.twitch.tv:80";
                    break;
                case ClientType.PubSub:
                    Url = options.UseWSS ? "wss://pubsub-edge.twitch.tv:443" : "ws://pubsub-edge.twitch.tv:80";
                    break;
            }

            InitializeClient();
            StartMonitor();
        }

        private void IncrementSentCount()
        {
            Interlocked.Increment(ref _sentCount);
        }

        private void InitializeClient()
        {
            _ws = new ClientWebSocket();

            DefaultKeepAliveInterval = Timeout.InfiniteTimeSpan;

            if (_options.Headers != null)
                foreach (var h in _options.Headers)
                {
                    try
                    {
                        _ws.Options.SetRequestHeader(h.Item1, h.Item2);
                    }
                    catch
                    { }
                }
        }

        /// <summary>
        /// Connect the Client to the requested Url.
        /// </summary>
        /// <returns>Returns True if Connected, False if Failed to Connect.</returns>
        public bool Open()
        {
            try
            {
                _disconnectCalled = false;
                _ws.ConnectAsync(new Uri(Url), _tokenSource.Token).Wait(15000);
                StartListener();
                StartSender();
                StartThrottlingWindowReset();

                Task.Run(() =>
                {
                    while (_ws.State != WebSocketState.Open)
                    { }
                }).Wait(15000);
                return _ws.State == WebSocketState.Open;
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, new OnErrorEventArgs { Exception = ex });
                throw;
            }
        }

        /// <summary>
        /// Queue a Message to Send to the server as a String.
        /// </summary>
        /// <param name="data">The Message To Queue</param>
        /// <returns>Returns True if was successfully queued. False if it fails.</returns>
        public bool Send(string data)
        {
            try
            {
                if (State != WebSocketState.Open || SendQueueLength >= _options.SendQueueCapacity)
                {
                    return false;
                }

                Task.Run(() =>
                {
                    _sendQueue.Add(new Tuple<DateTime, string>(DateTime.UtcNow, data));
                }).Wait(100, _tokenSource.Token);

                return true;
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, new OnErrorEventArgs { Exception = ex });
                throw;
            }
        }

        /// <summary>
        /// Queue a Message to Send to the server as a ByteArray.
        /// </summary>
        /// <param name="data">The Message To Queue</param>
        /// <returns>Returns True if was successfully queued. False if it fails.</returns>
        public bool Send(byte[] data)
        {
            return Send(Encoding.UTF8.GetString(data));
        }

        private void StartMonitor()
        {
            _monitor = Task.Run(() =>
            {
                _monitorRunning = true;
                var needsReconnect = false;
                try
                {
                    var lastState = State;
                    while (_ws != null && !_disposedValue)
                    {
                        if (lastState == State)
                        {
                            Thread.Sleep(200);
                            continue;
                        }
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { NewState = State, PreviousState = lastState});

                        if (State == WebSocketState.Open)
                            OnConnected?.Invoke(this, new OnConnectedEventArgs());

                        if ((State == WebSocketState.Closed || State == WebSocketState.Aborted) && !_reconnecting)
                        {
                            if (lastState == WebSocketState.Open && !_disconnectCalled && _options.ReconnectionPolicy != null && !_options.ReconnectionPolicy.AreAttemptsComplete())
                            {
                                needsReconnect = true;
                                break;
                            }
                            OnDisconnected?.Invoke(this, new OnDisconnectedEventArgs { Reason = _ws.CloseStatus ?? WebSocketCloseStatus.Empty });
                            if (_ws.CloseStatus != null && _ws.CloseStatus != WebSocketCloseStatus.NormalClosure)
                                OnError?.Invoke(this, new OnErrorEventArgs { Exception = new Exception(_ws.CloseStatus + " " + _ws.CloseStatusDescription) });
                        }

                        lastState = State;
                    }
                }
                catch (Exception ex)
                {
                    OnError?.Invoke(this, new OnErrorEventArgs { Exception = ex });
                }
                if (needsReconnect && !_reconnecting && !_disconnectCalled)
                    DoReconnect();
                _monitorRunning = false;
            });
        }

        private Task DoReconnect()
        {
            return Task.Run(() =>
            {
                _tokenSource.Cancel();
                _reconnecting = true;
                if (!Task.WaitAll(new[] {_monitor, _listener, _sender}, 15000))
                {
                    OnFatality?.Invoke(this, new OnFatalErrorEventArgs { Reason = "Fatal network error. Network services fail to shut down." });
                    _reconnecting = false;
                    _disconnectCalled = true;
                    _tokenSource.Cancel();
                    return;
                }
                _ws.Dispose();

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { NewState = WebSocketState.Connecting, PreviousState = WebSocketState.Aborted });

                _tokenSource = new CancellationTokenSource();

                var connected = false;
                while (!_disconnectCalled && !_disposedValue && !connected && !_tokenSource.IsCancellationRequested)
                    try
                    {
                        InitializeClient();
                        if (!_monitorRunning)
                        {
                            StartMonitor();
                        }
                        connected = _ws.ConnectAsync(new Uri(Url), _tokenSource.Token).Wait(15000);
                    }
                    catch
                    {
                        _ws.Dispose();
                        Thread.Sleep(_options.ReconnectionPolicy.GetReconnectInterval());
                        _options.ReconnectionPolicy.ProcessValues();
                        if (_options.ReconnectionPolicy.AreAttemptsComplete())
                        {
                            OnFatality?.Invoke(this, new OnFatalErrorEventArgs { Reason = "Fatal network error. Max reconnect attemps reached." });
                            _reconnecting = false;
                            _disconnectCalled = true;
                            _tokenSource.Cancel();
                            return;
                        }
                    }
                if (connected)
                {
                    _reconnecting = false;
                    if (!_monitorRunning)
                        StartMonitor();
                    if (!_listenerRunning)
                        StartListener();
                    if (!_senderRunning)
                        StartSender();
                    if (!_resetThrottlerRunning)
                        StartThrottlingWindowReset();
                }
            });
        }

        private void StartListener()
        {
            _listener = Task.Run(async () =>
            {
                _listenerRunning = true;
                try
                {
                    while (_ws.State == WebSocketState.Open && !_disposedValue && !_reconnecting)
                    {
                        var message = "";
                        var binary = new List<byte>();

                        READ:

                        var buffer = new byte[1024];
                        WebSocketReceiveResult res = null;

                        try
                        {
                            res = await _ws.ReceiveAsync(new ArraySegment<byte>(buffer), _tokenSource.Token);
                        }
                        catch
                        {
                            _ws.Abort();
                            break;
                        }

                        if (res == null)
                            goto READ;

                        if (res.MessageType == WebSocketMessageType.Close)
                        {
                            await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "SERVER REQUESTED CLOSE", _tokenSource.Token);
                        }

                        if (res.MessageType == WebSocketMessageType.Text)
                        {
                            if (!res.EndOfMessage)
                            {
                                message += Encoding.UTF8.GetString(buffer).TrimEnd('\0');
                                goto READ;
                            }
                            message += Encoding.UTF8.GetString(buffer).TrimEnd('\0');

                            if (message.Trim() == "ping")
                                Send("pong");
                            else
                            {
                                Task.Run(() => OnMessage?.Invoke(this, new OnMessageEventArgs { Message = message })).Wait(50);
                            }
                        }
                        else
                        {
                            if (!res.EndOfMessage)
                            {
                                binary.AddRange(buffer.Where(b => b != '\0'));
                                goto READ;
                            }

                            binary.AddRange(buffer.Where(b => b != '\0'));
                            Task.Run(() => OnData?.Invoke(this, new OnDataEventArgs { Data = binary.ToArray() })).Wait(50);
                        }
                        buffer = null;
                    }
                }
                catch (Exception ex)
                {
                    OnError?.Invoke(this, new OnErrorEventArgs { Exception = ex });
                }
                _listenerRunning = false;
                return Task.CompletedTask;
            });
        }

        private void StartSender()
        {
            _sender = Task.Run(async () =>
            {
                _senderRunning = true;
                try
                {
                    while (!_disposedValue && !_reconnecting)
                    {
                        await Task.Delay(_options.SendDelay);

                        if (_sentCount == _options.MessagesAllowedInPeriod)
                        {
                            OnMessageThrottled?.Invoke(this, new OnMessageThrottledEventArgs
                            {
                                Message = "Message Throttle Occured. Too Many Messages within the period specified in WebsocketClientOptions.",
                                AllowedInPeriod = _options.MessagesAllowedInPeriod,
                                Period = _options.ThrottlingPeriod,
                                SentMessageCount = Interlocked.CompareExchange(ref _sentCount, 0, 0)
                            });

                            continue;
                        }

                        if (_ws.State == WebSocketState.Open && !_reconnecting)
                        {
                            var msg = _sendQueue.Take(_tokenSource.Token);
                            if (msg.Item1.Add(_options.SendCacheItemTimeout) < DateTime.UtcNow)
                            {
                                continue;
                            }
                            var buffer = Encoding.UTF8.GetBytes(msg.Item2);
                            try
                            {
                                await _ws.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, _tokenSource.Token);
                                IncrementSentCount();
                            }
                            catch (Exception ex)
                            {
                                OnSendFailed?.Invoke(this, new OnSendFailedEventArgs { Data = msg.Item2, Exception = ex });
                                _ws.Abort();
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    OnSendFailed?.Invoke(this, new OnSendFailedEventArgs { Data = "", Exception = ex });
                    OnError?.Invoke(this, new OnErrorEventArgs { Exception = ex });
                }
                _senderRunning = false;
                return Task.CompletedTask;
            });
        }

        private void StartThrottlingWindowReset()
        {
            _resetThrottler = Task.Run(async () => {
                _resetThrottlerRunning = true;
                while (!_disposedValue && !_reconnecting)
                {
                    Interlocked.Exchange(ref _sentCount, 0);
                    await Task.Delay(_options.ThrottlingPeriod);
                }
                _resetThrottlerRunning = false;
                return Task.CompletedTask;
            });
        }

        /// <summary>
        /// Disconnect the Client from the Server
        /// </summary>
        public void Close()
        {
            try
            {
                _disconnectCalled = true;
                _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "NORMAL SHUTDOWN", _tokenSource.Token).Wait(_options.DisconnectWait);
            }
            catch
            { }
        }

        #region IDisposable Support

        private bool _disposedValue;

        protected virtual void Dispose(bool disposing, bool waitForSendsToComplete)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (_sendQueue.Count > 0 && _senderRunning)
                    {
                        var i = 0;
                        while (_sendQueue.Count > 0 && _senderRunning)
                        {
                            i++;
                            Task.Delay(1000).Wait();
                            if(i > 25)
                                break;
                        }
                    }
                    Close();
                    _tokenSource.Cancel();
                    Thread.Sleep(500);
                    _tokenSource.Dispose();
                    _ws.Dispose();
                    GC.Collect();
                }

                _disposedValue = true;
            }
        }

        /// <summary>
        /// Dispose the Client. Forces the Send Queue to be destroyed, resulting in Message Loss.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }


        /// <summary>
        /// Disposes the Client. Waits for current Messages in the Queue to be processed first.
        /// </summary>
        /// <param name="waitForSendsToComplete">Should wait or not.</param>
        public void Dispose(bool waitForSendsToComplete)
        {
            Dispose(true, waitForSendsToComplete);
        }

        #endregion
    }
}