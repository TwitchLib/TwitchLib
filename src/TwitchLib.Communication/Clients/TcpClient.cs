using System;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Communication.Events;
using TwitchLib.Communication.Interfaces;
using TwitchLib.Communication.Models;
using TwitchLib.Communication.Services;

namespace TwitchLib.Communication.Clients
{
    public class TcpClient : IClient
    {
        public TimeSpan DefaultKeepAliveInterval { get; set; }
        public int SendQueueLength => _throttlers.SendQueue.Count;
        public int WhisperQueueLength => _throttlers.WhisperQueue.Count;
        public bool IsConnected => Client?.Connected ?? false;
        public IClientOptions Options { get; }

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

        private readonly string _server = "irc.chat.twitch.tv";
        private int Port => Options != null ? Options.UseSsl ? 443 : 80 : 0;
        public System.Net.Sockets.TcpClient Client { get; private set; }
        private StreamReader _reader;
        private StreamWriter _writer;
        private readonly Throttlers _throttlers;
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private bool _stopServices;
        private bool _networkServicesRunning;
        private Task[] _networkTasks;
        private Task _monitorTask;

        public TcpClient(IClientOptions options = null)
        {
            Options = options ?? new ClientOptions();
            _throttlers =
                new Throttlers(this, Options.ThrottlingPeriod, Options.WhisperThrottlingPeriod)
                {
                    TokenSource = _tokenSource
                };
            InitializeClient();
        }

        private void InitializeClient()
        {
            Client = new System.Net.Sockets.TcpClient();

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

                Task.Run(() => { 
                InitializeClient();
                Client.Connect(_server, Port);
                if (Options.UseSsl)
                {
                    var ssl = new SslStream(Client.GetStream(), false);
                    ssl.AuthenticateAsClient(_server);
                    _reader = new StreamReader(ssl);
                    _writer = new StreamWriter(ssl);
                }
                else
                {
                    _reader = new StreamReader(Client.GetStream());
                    _writer = new StreamWriter(Client.GetStream());
                }
                }).Wait(10000);

                if (!IsConnected) return Open();
                
                StartNetworkServices();
                return true;

            }
            catch (Exception)
            {
                InitializeClient();
                return false;
            }
        }

        public void Close(bool callDisconnect = true)
        {
            _reader?.Dispose();
            _writer?.Dispose();
            Client?.Close();

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
                OnError?.Invoke(this, new OnErrorEventArgs {Exception = ex});
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
                OnError?.Invoke(this, new OnErrorEventArgs {Exception = ex});
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

        public Task SendAsync(string message)
        {
            return Task.Run(async () =>
            {
                await _writer.WriteLineAsync(message);
                await _writer.FlushAsync();
            });
        }

        private Task StartListenerTask()
        {
            return Task.Run(async () =>
            {
                while (IsConnected && _networkServicesRunning)
                {
                    try
                    {
                        var input = await _reader.ReadLineAsync();
                        OnMessage?.Invoke(this, new OnMessageEventArgs {Message = input});
                    }
                    catch (Exception ex)
                    {
                        OnError?.Invoke(this, new OnErrorEventArgs {Exception = ex});
                    }
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
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { IsConnected = IsConnected, WasConnected = lastState });

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
                        }

                        lastState = IsConnected;
                    }
                }
                catch (Exception ex)
                {
                    OnError?.Invoke(this, new OnErrorEventArgs {Exception = ex});
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
