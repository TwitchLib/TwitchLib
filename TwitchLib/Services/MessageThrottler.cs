using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TwitchLib.Services
{
    public class MessageThrottler
    {
        private Timer _periodTimer = new Timer();
        private TimeSpan _periodDuration;
        private int _currentMessageCount;

        public enum ThrottleType
        {
            TooManyMessages = 0,
            MessageTooShort = 1,
            MessageTooLong = 2
        }

        public int MessagesAllowedInPeriod { get; set; }
        public TimeSpan PeriodDuration { get { return _periodDuration; } set { _periodDuration = value; _periodTimer.Interval = value.TotalMilliseconds; } }
        public int MinimumMessageLengthAllowed { get; set; }
        public int MaximumMessageLengthAllowed { get; set; }
        public bool ApplyThrottlingToRawMessages { get; set; }

        /// <summary>messageThrottler constructor.</summary>
        public MessageThrottler(int messagesAllowedInPeriod, TimeSpan periodDuration, bool applyThrottlingToRawMessages = false, int minimumMessageLengthAllowed = -1, int maximumMessageLengthAllowed = -1) 
        {
            MessagesAllowedInPeriod = messagesAllowedInPeriod;
            PeriodDuration = periodDuration;
            MinimumMessageLengthAllowed = minimumMessageLengthAllowed;
            MaximumMessageLengthAllowed = maximumMessageLengthAllowed;

            _periodTimer.Elapsed += periodTimerElapsed;
        }

        /// <summary>Function that verifies a message is legal, returns true/false on message legality.</summary>
        public bool MessagePermitted(string message)
        {
            if (!_periodTimer.Enabled)
                _periodTimer.Start();
            if(message.Length > MaximumMessageLengthAllowed && MaximumMessageLengthAllowed != -1)
            {
                OnClientThrottled?.Invoke(null,
                    new OnClientThrottledArgs { Message = message, PeriodDuration = PeriodDuration,
                        ThrottleViolation = ThrottleType.MessageTooLong });
                return false;
            }
            if(message.Length < MinimumMessageLengthAllowed)
            {
                OnClientThrottled?.Invoke(null,
                    new OnClientThrottledArgs { Message = message, PeriodDuration = PeriodDuration,
                        ThrottleViolation = ThrottleType.MessageTooShort });
                return false;
            }
            if(_currentMessageCount == MessagesAllowedInPeriod)
            {
                OnClientThrottled?.Invoke(null,
                    new OnClientThrottledArgs { Message = message, PeriodDuration = PeriodDuration,
                        ThrottleViolation = ThrottleType.TooManyMessages });
                return false;
            }

            _currentMessageCount++;
            return true;
        }

        #region HELPERS
        private void periodTimerElapsed(object sender, ElapsedEventArgs e)
        {
            _periodTimer.Stop();
            OnThrottledPeriodReset?.Invoke(null,
                new OnThrottlePeriodResetArgs { TimeInPeriod = PeriodDuration });
            _currentMessageCount = 0;
        }
        #endregion

        #region EVENTS
        /// <summary>Event fires when service starts.</summary>
        public event EventHandler<OnClientThrottledArgs> OnClientThrottled;
        /// <summary>Event fires when service stops.</summary>
        public event EventHandler<OnThrottlePeriodResetArgs> OnThrottledPeriodReset;

        /// <summary>Class representing event args for OnClientThrottled.</summary>
        public class OnClientThrottledArgs : EventArgs
        {
            /// <summary>Event property representing reason why message was throttled.</summary>
            public ThrottleType ThrottleViolation;
            /// <summary>Event property representing message that failed to send due to throttling.</summary>
            public string Message;
            /// <summary>Event property representing message that failed to send due to throttling.</summary>
            public TimeSpan PeriodDuration;
        }

        /// <summary>Class representing event args for OnServiceStopped event.</summary>
        public class OnThrottlePeriodResetArgs : EventArgs
        {
            /// <summary>Event property representing number of seconds in new throttle period.</summary>
            public TimeSpan TimeInPeriod;
        }
        #endregion
    }
}
