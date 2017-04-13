using System;

namespace TwitchLib.Events.Services.MessageThrottler
{
    /// <summary>Class representing event args for OnServiceStopped event.</summary>
    public class OnThrottlePeriodResetArgs : EventArgs
    {
        /// <summary>Event property representing number of seconds in new throttle period.</summary>
        public TimeSpan TimeInPeriod;
    }
}
