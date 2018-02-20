#region using directives
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Enums;
using TwitchLib.Events.Services.MessageThrottler;
using TwitchLib.Models.Client;
#endregion

namespace TwitchLib.Services
{
    /// <summary>Class used to throttle Regular Channel Chat Messages to enforce guidelines.</summary>
    public class MessageThrottler
    {
        #region Private Properties
        private readonly TimeSpan _periodDuration;
        private readonly Random _nonceRand;
        private readonly ConcurrentQueue<OutgoingMessage> _pendingSends;
        private readonly ConcurrentDictionary<int, string> _pendingSendsByNonce;
        private int _sentCount;
        private readonly int _messageLimit;
        #endregion

        #region Public Properties
        public int Count => _sentCount;
        public int PendingSendCount => _pendingSends.Count;
        /// <summary>Property representing number of messages allowed before throttling in a period.</summary>
        public int MessagesAllowedInPeriod { get; set; }
        /// <summary>Property representing minimum message length for throttling.</summary>
        public int MinimumMessageLengthAllowed { get; set; }
        /// <summary>Property representing maximum message length before throttling.</summary>
        public int MaximumMessageLengthAllowed { get; set; }
        /// <summary>Property representing whether throttling should be applied to raw messages.</summary>
        public bool ApplyThrottlingToRawMessages { get; set; }
        public ITwitchClient Client { get; }
        public CancellationTokenSource CancellationTokenSource { get; set; }
        public CancellationToken CancellationToken { get; set; }
        #endregion

        #region Events
        /// <summary>Event fires when service starts.</summary>
        public event EventHandler<OnClientThrottledArgs> OnClientThrottled;
        #endregion

        #region Constructor
        /// <summary>MessageThrottler constructor.</summary>
        public MessageThrottler(ITwitchClient client, int messagesAllowedInPeriod, TimeSpan periodDuration, bool applyThrottlingToRawMessages = false, int minimumMessageLengthAllowed = -1, int maximumMessageLengthAllowed = -1)
        {
            Client = client;
            MessagesAllowedInPeriod = messagesAllowedInPeriod;
            MinimumMessageLengthAllowed = minimumMessageLengthAllowed;
            MaximumMessageLengthAllowed = maximumMessageLengthAllowed;
            _periodDuration = periodDuration;
            _messageLimit = messagesAllowedInPeriod;
            _nonceRand = new Random();
            _pendingSends = new ConcurrentQueue<OutgoingMessage>();
            _pendingSendsByNonce = new ConcurrentDictionary<int, string>();
            _sentCount = 0;
        }
        #endregion

        #region Public Methods
        public Task StartQueue()
        {
            CancellationTokenSource = new CancellationTokenSource();
            CancellationToken = CancellationTokenSource.Token;
            return Task.Run(() => {
                StartResetTask(CancellationToken);
                RunQueue(CancellationToken);
            }, CancellationToken);
        }

        public void StopQueue()
        {
            CancellationTokenSource.Cancel();
            Clear();
        }

        public OutgoingMessage QueueSend(string message)
        {
            if (!MessagePermitted(message)) return new OutgoingMessage
            {
                Message = message,
                Sender = Client.TwitchUsername,
                State = MessageState.Failed
            };

            var msg = new OutgoingMessage()
            {
                Nonce = GenerateNonce(),
                Message = message,
                Sender = Client.TwitchUsername,
                Channel = Client.JoinedChannels.FirstOrDefault()?.Channel
            };

            if (_pendingSendsByNonce.TryAdd(msg.Nonce, msg.Message))
            {
                msg.State = MessageState.Queued;
                _pendingSends.Enqueue(msg);
            }
            else
                msg.State = MessageState.Failed;
            return msg;
        }

        public void Clear()
        {
            while (_pendingSends.TryDequeue(out var msg))
            { }

            _pendingSendsByNonce.Clear();
        }
        #endregion

        #region Internal Methods
        private Task RunQueue(CancellationToken cancelToken)
        {
            return Task.Run(async () =>
            {
                try
                {
                    while (!cancelToken.IsCancellationRequested)
                    {
                        await Task.Delay(250, cancelToken).ConfigureAwait(false);
                        if (_sentCount == _messageLimit) continue;

                        while (_pendingSends.TryDequeue(out var msg))
                        {
                            IncrementCount();
                            if (!_pendingSendsByNonce.TryRemove(msg.Nonce, out var _)) continue;
                            try
                            {
                                Client.SendQueuedItem(msg.Message);
                                msg.State = MessageState.Normal;
                                break;
                            }
                            catch (Exception ex)
                            {
                                msg.State = MessageState.Failed;
                                Client.Log($"Failed to send message to {msg.Channel}, Error: {ex.Message}");
                            }
                        }
                    }
                }
                catch (OperationCanceledException) { }
            }, cancelToken);
        }
        private Task StartResetTask(CancellationToken token)
        {
            return Task.Run(async () => {
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(_periodDuration, token);
                    Interlocked.Exchange(ref _sentCount, 0);
                }
            }, token);
        }
        private int GenerateNonce()
        {
            lock (_nonceRand)
                return _nonceRand.Next(1, int.MaxValue);
        }

        private void IncrementCount()
        {
            Interlocked.Increment(ref _sentCount);
        }

        private bool MessagePermitted(string message)
        {
            if (message.Length > MaximumMessageLengthAllowed && MaximumMessageLengthAllowed != -1)
            {
                OnClientThrottled?.Invoke(this,
                    new OnClientThrottledArgs
                    {
                        Message = message,
                        ThrottleViolation = ThrottleType.MessageTooLong
                    });
                return false;
            }

            if (message.Length >= MinimumMessageLengthAllowed) return true;

            OnClientThrottled?.Invoke(this,
                new OnClientThrottledArgs
                {
                    Message = message,
                    ThrottleViolation = ThrottleType.MessageTooShort
                });
            return false;

        }
        #endregion
    }
}