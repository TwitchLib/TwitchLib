namespace TwitchLib.Events.Services.MessageThrottler
{
    #region using directives
    using System;
    #endregion
    /// <summary>Class representing event args for OnClientThrottled.</summary>
    public class OnClientThrottledArgs : EventArgs
    {
        /// <summary>Event property representing reason why message was throttled.</summary>
        public Enums.ThrottleType ThrottleViolation;
        /// <summary>Event property representing message that failed to send due to throttling.</summary>
        public string Message;
        /// <summary>Event property representing message that failed to send due to throttling.</summary>
        public TimeSpan PeriodDuration;
    }
}
