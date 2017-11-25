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
        private TimeSpan _periodDuration;
        private readonly Random _nonceRand;
        private readonly ConcurrentQueue<OutgoingMessage> _pendingSends;
        private readonly ConcurrentDictionary<int, string> _pendingSendsByNonce;
        private int _count;
        private int _messageLimit;
        private int _messageLimitCounter;
        #endregion

        #region Public Properties
        public int Count => _count;
        public int PendingSendCount => _pendingSends.Count;
        /// <summary>Property representing number of messages allowed before throttling in a period.</summary>
        public int MessagesAllowedInPeriod { get; set; }
        /// <summary>Property representing minimum message length for throttling.</summary>
        public int MinimumMessageLengthAllowed { get; set; }
        /// <summary>Property representing maximum message length before throttling.</summary>
        public int MaximumMessageLengthAllowed { get; set; }
        /// <summary>Property representing whether throttling should be applied to raw messages.</summary>
        public bool ApplyThrottlingToRawMessages { get; set; }
        public TwitchClient Client { get; }
        public CancellationTokenSource CancellationTokenSource { get; set; }
        public CancellationToken CancellationToken { get; set; }
        #endregion

        #region Events
        /// <summary>Event fires when service starts.</summary>
        public event EventHandler<OnClientThrottledArgs> OnClientThrottled;
        #endregion

        #region Constructor
        /// <summary>MessageThrottler constructor.</summary>
        public MessageThrottler(TwitchClient client, int messagesAllowedInPeriod, TimeSpan periodDuration, bool applyThrottlingToRawMessages = false, int minimumMessageLengthAllowed = -1, int maximumMessageLengthAllowed = -1)
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
        }
        #endregion

        #region Public Methods
        public void StartQueue()
        {
            CancellationTokenSource = new CancellationTokenSource();
            CancellationToken = CancellationTokenSource.Token;

            StartResetTask(CancellationToken);
            RunQueue(CancellationToken);
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
                IncrementCount();
                _pendingSends.Enqueue(msg);
            }
            else
                msg.State = MessageState.Failed;
            return msg;
        }

        public void Clear()
        {
            while (_pendingSends.TryDequeue(out OutgoingMessage msg))
                DecrementCount();
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
                        await Task.Delay(250).ConfigureAwait(false);
                        if (_messageLimitCounter == _messageLimit) continue;
                        while (_pendingSends.TryDequeue(out OutgoingMessage msg))
                        {
                            DecrementCount();
                            if (_pendingSendsByNonce.TryRemove(msg.Nonce, out string message))
                            {
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
                }
                catch (OperationCanceledException) { }
            });
        }
        private Task StartResetTask(CancellationToken _token)
        {
            return Task.Run(async () => {
                while (!_token.IsCancellationRequested)
                {
                    await Task.Delay(_periodDuration);
                    _count = 0;
                }
            });
        }
        private int GenerateNonce()
        {
            lock (_nonceRand)
                return _nonceRand.Next(1, int.MaxValue);
        }
        
        private void IncrementCount()
        {
            int count = System.Threading.Interlocked.Increment(ref _count);
            if (count >= _messageLimitCounter)
            {
                _messageLimitCounter <<= 1;
                count = _pendingSends.Count;
                Client.Log($"Queue is backed up, currently at ({count} sends.");
            }
            else if (count < _messageLimit)
                _messageLimitCounter = _messageLimit;
        }
        private void DecrementCount()
        {
            int count = System.Threading.Interlocked.Decrement(ref _count);
            if (count < (_messageLimit / 2))
                _messageLimitCounter = _messageLimit;
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
            if (message.Length < MinimumMessageLengthAllowed)
            {
                OnClientThrottled?.Invoke(this,
                    new OnClientThrottledArgs
                    {
                        Message = message,
                        ThrottleViolation = ThrottleType.MessageTooShort
                    });
                return false;
            }
            
            return true;
        }
        #endregion
    }
}
