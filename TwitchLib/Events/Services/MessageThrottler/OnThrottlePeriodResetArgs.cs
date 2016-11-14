using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Events.Services.MessageThrottler
{
    /// <summary>Class representing event args for OnServiceStopped event.</summary>
    public class OnThrottlePeriodResetArgs : EventArgs
    {
        /// <summary>Event property representing number of seconds in new throttle period.</summary>
        public TimeSpan TimeInPeriod;
    }
}
