using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Events.Services.MessageThrottler
{
    /// <summary>Class representing event args for OnClientThrottled.</summary>
    public class OnClientThrottledArgs : EventArgs
    {
        /// <summary>Event property representing reason why message was throttled.</summary>
        public Common.ThrottleType ThrottleViolation;
        /// <summary>Event property representing message that failed to send due to throttling.</summary>
        public string Message;
        /// <summary>Event property representing message that failed to send due to throttling.</summary>
        public TimeSpan PeriodDuration;
    }
}
