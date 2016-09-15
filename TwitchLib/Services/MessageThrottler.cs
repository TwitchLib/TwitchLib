using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TwitchLib.Services
{
    /// <summary>Class used to throttle chat and whsiper messages to enforce guidelines.</summary>
    public class MessageThrottler
    {
        private Timer _periodTimer = new Timer();
        private TimeSpan _periodDuration;
        private int _currentMessageCount;

        /// <summary>Enum representing the available throttle types.</summary>
        public enum ThrottleType
        {
            /// <summary>Throttle based on too many messages.</summary>
            TooManyMessages = 0,
            /// <summary>Throttle based on message being too short.</summary>
            MessageTooShort = 1,
            /// <summary>Throttle based on message being too long.</summary>
            MessageTooLong = 2
        }

        /// <summary>Property representing number of messages allowed before throttling in a period.</summary>
        public int MessagesAllowedInPeriod { get; set; }
        /// <summary>Property representing the time period for throttling.</summary>
        public TimeSpan PeriodDuration { get { return _periodDuration; } set { _periodDuration = value; _periodTimer.Interval = value.TotalMilliseconds; } }
        /// <summary>Property representing minimum message length for throttling.</summary>
        public int MinimumMessageLengthAllowed { get; set; }
        /// <summary>Property representing maximum message length before throttling.</summary>
        public int MaximumMessageLengthAllowed { get; set; }
        /// <summary>Property representing whether throttling should be applied to raw messages.</summary>
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
                OnClientThrottled?.Invoke(this,
                    new OnClientThrottledArgs { Message = message, PeriodDuration = PeriodDuration,
                        ThrottleViolation = ThrottleType.MessageTooLong });
                return false;
            }
            if(message.Length < MinimumMessageLengthAllowed)
            {
                OnClientThrottled?.Invoke(this,
                    new OnClientThrottledArgs { Message = message, PeriodDuration = PeriodDuration,
                        ThrottleViolation = ThrottleType.MessageTooShort });
                return false;
            }
            if(_currentMessageCount == MessagesAllowedInPeriod)
            {
                OnClientThrottled?.Invoke(this,
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
            OnThrottledPeriodReset?.Invoke(this,
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
