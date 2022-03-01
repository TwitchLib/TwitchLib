using System;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Communication.Enums;
using TwitchLib.Communication.Events;
using TwitchLib.Communication.Interfaces;
using TwitchLib.Communication.Models;
using TwitchLib.Communication.Services;

namespace TwitchLib.Communication.Clients
{
    public class WebSocketClient : IClient
    {
        public TimeSpan DefaultKeepAliveInterval { get; set; }
        public int SendQueueLength => _throttlers.SendQueue.Count;
        public int WhisperQueueLength => _throttlers.WhisperQueue.Count;
        public bool IsConnected => Client?.State == WebSocketState.Open;
        public IClientOptions Options { get; }
        public ClientWebSocket Client { get; private set; }

        public event EventHandler<OnConnectedEventArgs> OnConnected;
        public event EventHandler<OnDataEventArgs> OnData;
        public event EventHandler<OnDisconnectedEventArgs> OnDisconnected;
        public event EventHandler<OnErrorEventArgs> OnError;
        public event EventHandler<OnFatalErrorEventArgs> OnFatality;
        public event EventHandler<OnMessageEventArgs> OnMessage;
        public event EventHandler<OnMessageThrottledEventArgs> OnMessageThrottled;
        public event EventHandler<OnWhisperThrottledEventArgs> OnWhisperThrottled;
        public event EventHandler<OnSendFailedEventArgs> OnSendFailed;
        public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
        public event EventHandler<OnReconnectedEventArgs> OnReconnected;

        private string Url { get; }
        private readonly Throttlers _throttlers;
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private bool _stopServices;
        private bool _networkServicesRunning;
        private Task[] _networkTasks;
        private Task _monitorTask;
        
        public WebSocketClient(IClientOptions options = null)
        {
            Options = options ?? new ClientOptions();

            switch (Options.ClientType)
            {
                case ClientType.Chat:
                    Url = Options.UseSsl ? "wss://irc-ws.chat.twitch.tv:443" : "ws://irc-ws.chat.twitch.tv:80";
                    break;
                case ClientType.PubSub:
                    Url = Options.UseSsl ? "wss://pubsub-edge.twitch.tv:443" : "ws://pubsub-edge.twitch.tv:80";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _throttlers = new Throttlers(this, Options.ThrottlingPeriod, Options.WhisperThrottlingPeriod) { TokenSource = _tokenSource };
        }

        private void InitializeClient()
        {
            Client?.Abort();
            Client = new ClientWebSocket();
            
            if (_monitorTask == null)
            {
                _monitorTask = StartMonitorTask();
                return;
            }

            if (_monitorTask.IsCompleted) _monitorTask = StartMonitorTask();
        }

        public bool Open()
        {
            try
            {
                if (IsConnected) return true;

                InitializeClient();
                Client.ConnectAsync(new Uri(Url), _tokenSource.Token).Wait(10000);
                if (!IsConnected) return Open();
                
                StartNetworkServices();
                return true;
            }
            catch (WebSocketException)
            {
                InitializeClient();
                return false;
            }
        }

        public void Close(bool callDisconnect = true)
        {
            Client?.Abort();
            _stopServices = callDisconnect;
            CleanupServices();
            InitializeClient();
            OnDisconnected?.Invoke(this, new OnDisconnectedEventArgs());
        }
        
        public void Reconnect()
        {
            Close();
            if(Open())
            {
                OnReconnected?.Invoke(this, new OnReconnectedEventArgs());
            }
        }
        
        public bool Send(string message)
        {
            try
            {
                if (!IsConnected || SendQueueLength >= Options.SendQueueCapacity)
                {
                    return false;
                }

                _throttlers.SendQueue.Add(new Tuple<DateTime, string>(DateTime.UtcNow, message));

                return true;
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, new OnErrorEventArgs { Exception = ex });
                throw;
            }
        }
        
        public bool SendWhisper(string message)
        {
            try
            {
                if (!IsConnected || WhisperQueueLength >= Options.WhisperQueueCapacity)
                {
                    return false;
                }

                _throttlers.WhisperQueue.Add(new Tuple<DateTime, string>(DateTime.UtcNow, message));

                return true;
            }
            catch (Exception ex)
            {
                OnError?.Invoke(this, new OnErrorEventArgs { Exception = ex });
                throw;
            }
        }
        
        private void StartNetworkServices()
        {
            _networkServicesRunning = true;
            _networkTasks = new[]
            {
                StartListenerTask(),
                _throttlers.StartSenderTask(),
                _throttlers.StartWhisperSenderTask()
            }.ToArray();

            if (!_networkTasks.Any(c => c.IsFaulted)) return;
            _networkServicesRunning = false;
            CleanupServices();
        }

        public Task SendAsync(byte[] message)
        {
            return Client.SendAsync(new ArraySegment<byte>(message), WebSocketMessageType.Text, true, _tokenSource.Token);
        }

        private Task StartListenerTask()
        {
            return Task.Run(async () =>
            {
                var message = "";

                while (IsConnected && _networkServicesRunning)
                {
                    WebSocketReceiveResult result;
                    var buffer = new byte[1024];

                    try
                    {
                        result = await Client.ReceiveAsync(new ArraySegment<byte>(buffer), _tokenSource.Token);
                    }
                    catch
                    {
                        InitializeClient();
                        break;
                    }

                    if (result == null) continue;

                    switch (result.MessageType)
                    {
                        case WebSocketMessageType.Close:
                            Close();
                            break;
                        case WebSocketMessageType.Text when !result.EndOfMessage:
                            message += Encoding.UTF8.GetString(buffer).TrimEnd('\0');
                            continue;
                        case WebSocketMessageType.Text:
                            message += Encoding.UTF8.GetString(buffer).TrimEnd('\0');
                            OnMessage?.Invoke(this, new OnMessageEventArgs(){Message = message});
                            break;
                        case WebSocketMessageType.Binary:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    
                    message = "";
                }
            });
        }

        private Task StartMonitorTask()
        {
            return Task.Run(() =>
            {
                var needsReconnect = false;
                try
                {
                    var lastState = IsConnected;
                    while (!_tokenSource.IsCancellationRequested)
                    {
                        if (lastState == IsConnected)
                        {
                            Thread.Sleep(200);
                            continue;
                        }
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { IsConnected = Client.State == WebSocketState.Open, WasConnected = lastState});

                        if (IsConnected)
                            OnConnected?.Invoke(this, new OnConnectedEventArgs());

                        if (!IsConnected && !_stopServices)
                        {
                            if (lastState && Options.ReconnectionPolicy != null && !Options.ReconnectionPolicy.AreAttemptsComplete())
                            {
                                needsReconnect = true;
                                break;
                            }
                            
                            OnDisconnected?.Invoke(this, new OnDisconnectedEventArgs());
                            if (Client.CloseStatus != null && Client.CloseStatus != WebSocketCloseStatus.NormalClosure)
                                OnError?.Invoke(this, new OnErrorEventArgs { Exception = new Exception(Client.CloseStatus + " " + Client.CloseStatusDescription) });
                        }

                        lastState = IsConnected;
                    }
                }
                catch (Exception ex)
                {
                    OnError?.Invoke(this, new OnErrorEventArgs { Exception = ex });
                }

                if (needsReconnect && !_stopServices)
                    Reconnect();
            }, _tokenSource.Token);
        }

        private void CleanupServices()
        {
            _tokenSource.Cancel();
            _tokenSource = new CancellationTokenSource();
            _throttlers.TokenSource = _tokenSource;
            
            if (!_stopServices) return;
            if (!(_networkTasks?.Length > 0)) return;
            if (Task.WaitAll(_networkTasks, 15000)) return;
           
            OnFatality?.Invoke(this,
                new OnFatalErrorEventArgs
                {
                    Reason = "Fatal network error. Network services fail to shut down."
                });
            _stopServices = false;
            _throttlers.Reconnecting = false;
            _networkServicesRunning = false;
        }
       
        public void WhisperThrottled(OnWhisperThrottledEventArgs eventArgs)
        {
            OnWhisperThrottled?.Invoke(this, eventArgs);
        }

        public void MessageThrottled(OnMessageThrottledEventArgs eventArgs)
        {
            OnMessageThrottled?.Invoke(this, eventArgs);
        }

        public void SendFailed(OnSendFailedEventArgs eventArgs)
        {
            OnSendFailed?.Invoke(this, eventArgs);
        }

        public void Error(OnErrorEventArgs eventArgs)
        {
            OnError?.Invoke(this, eventArgs);
        }

        public void Dispose()
        {
            Close();
            _throttlers.ShouldDispose = true;
            _tokenSource.Cancel();
            Thread.Sleep(500);
            _tokenSource.Dispose();
            Client?.Dispose();
            GC.Collect();
        }
    }
}
