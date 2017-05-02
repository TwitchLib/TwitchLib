namespace TwitchLib.Events.Services.MessageThrottler
{
    #region using directives
    using System;
    #endregion
    /// <summary>Class representing event args for OnServiceStopped event.</summary>
    public class OnThrottlePeriodResetArgs : EventArgs
    {
        /// <summary>Event property representing number of seconds in new throttle period.</summary>
        public TimeSpan TimeInPeriod;
    }
}
