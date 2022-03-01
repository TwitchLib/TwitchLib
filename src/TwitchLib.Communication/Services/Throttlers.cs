using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Events;
using TwitchLib.Communication.Interfaces;

namespace TwitchLib.Communication.Services
{
    public class Throttlers
    {
        public readonly BlockingCollection<Tuple<DateTime, string>> SendQueue =
            new BlockingCollection<Tuple<DateTime, string>>();

        public readonly BlockingCollection<Tuple<DateTime, string>> WhisperQueue =
            new BlockingCollection<Tuple<DateTime, string>>();

        public bool Reconnecting { get; set; } = false;
        public bool ShouldDispose { get; set; } = false;
        public CancellationTokenSource TokenSource { get; set; }
        public bool ResetThrottlerRunning;
        public bool ResetWhisperThrottlerRunning;
        public int SentCount = 0;
        public int WhispersSent = 0;
        public Task ResetThrottler;
        public Task ResetWhisperThrottler;

        private readonly TimeSpan _throttlingPeriod;
        private readonly TimeSpan _whisperThrottlingPeriod;
        private readonly IClient _client;

        public Throttlers(IClient client, TimeSpan throttlingPeriod, TimeSpan whisperThrottlingPeriod)
        {
            _throttlingPeriod = throttlingPeriod;
            _whisperThrottlingPeriod = whisperThrottlingPeriod;
            _client = client;
        }

        public void StartThrottlingWindowReset()
        {
            ResetThrottler = Task.Run(async () =>
            {
                ResetThrottlerRunning = true;
                while (!ShouldDispose && !Reconnecting)
                {
                    Interlocked.Exchange(ref SentCount, 0);
                    await Task.Delay(_throttlingPeriod, TokenSource.Token);
                }

                ResetThrottlerRunning = false;
                return Task.CompletedTask;
            });
        }

        public void StartWhisperThrottlingWindowReset()
        {
            ResetWhisperThrottler = Task.Run(async () =>
            {
                ResetWhisperThrottlerRunning = true;
                while (!ShouldDispose && !Reconnecting)
                {
                    Interlocked.Exchange(ref WhispersSent, 0);
                    await Task.Delay(_whisperThrottlingPeriod, TokenSource.Token);
                }

                ResetWhisperThrottlerRunning = false;
                return Task.CompletedTask;
            });
        }

        public void IncrementSentCount()
        {
            Interlocked.Increment(ref SentCount);
        }

        public void IncrementWhisperCount()
        {
            Interlocked.Increment(ref WhispersSent);
        }

        public Task StartSenderTask()
        {
            StartThrottlingWindowReset();
            
            return Task.Run(async () =>
            {
                try
                {
                    while (!ShouldDispose)
                    {
                        await Task.Delay(_client.Options.SendDelay);

                        if (SentCount == _client.Options.MessagesAllowedInPeriod)
                        {
                            _client.MessageThrottled(new OnMessageThrottledEventArgs
                            {
                                Message =
                                    "Message Throttle Occured. Too Many Messages within the period specified in WebsocketClientOptions.",
                                AllowedInPeriod = _client.Options.MessagesAllowedInPeriod,
                                Period = _client.Options.ThrottlingPeriod,
                                SentMessageCount = Interlocked.CompareExchange(ref SentCount, 0, 0)
                            });

                            continue;
                        }

                        if (!_client.IsConnected || ShouldDispose) continue;

                        var msg = SendQueue.Take(TokenSource.Token);
                        if (msg.Item1.Add(_client.Options.SendCacheItemTimeout) < DateTime.UtcNow) continue;

                        try
                        {
                            switch (_client)
                            {
                                case WebSocketClient ws:
                                    await ws.SendAsync(Encoding.UTF8.GetBytes(msg.Item2));
                                    break;
                                case TcpClient tcp:
                                    await tcp.SendAsync(msg.Item2);
                                    break;
                            }

                            IncrementSentCount();
                        }
                        catch (Exception ex)
                        {
                            _client.SendFailed(new OnSendFailedEventArgs {Data = msg.Item2, Exception = ex});
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _client.SendFailed(new OnSendFailedEventArgs {Data = "", Exception = ex});
                    _client.Error(new OnErrorEventArgs {Exception = ex});
                }
            });
        }

        public Task StartWhisperSenderTask()
        {
            StartWhisperThrottlingWindowReset();
            
            return Task.Run(async () =>
            {
                try
                {
                    while (!ShouldDispose)
                    {
                        await Task.Delay(_client.Options.SendDelay);

                        if (WhispersSent == _client.Options.WhispersAllowedInPeriod)
                        {
                            _client.WhisperThrottled(new OnWhisperThrottledEventArgs()
                            {
                                Message =
                                    "Whisper Throttle Occured. Too Many Whispers within the period specified in ClientOptions.",
                                AllowedInPeriod = _client.Options.WhispersAllowedInPeriod,
                                Period = _client.Options.WhisperThrottlingPeriod,
                                SentWhisperCount = Interlocked.CompareExchange(ref WhispersSent, 0, 0)
                            });

                            continue;
                        }

                        if (!_client.IsConnected || ShouldDispose) continue;

                        var msg = WhisperQueue.Take(TokenSource.Token);
                        if (msg.Item1.Add(_client.Options.SendCacheItemTimeout) < DateTime.UtcNow) continue;

                        try
                        {
                            switch (_client)
                            {
                                case WebSocketClient ws:
                                    await ws.SendAsync(Encoding.UTF8.GetBytes(msg.Item2));
                                    break;
                                case TcpClient tcp:
                                    await tcp.SendAsync(msg.Item2);
                                    break;
                            }

                            IncrementSentCount();
                        }
                        catch (Exception ex)
                        {
                            _client.SendFailed(new OnSendFailedEventArgs {Data = msg.Item2, Exception = ex});
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _client.SendFailed(new OnSendFailedEventArgs {Data = "", Exception = ex});
                    _client.Error(new OnErrorEventArgs {Exception = ex});
                }
            });
        }
    }
}
