using System;

namespace TwitchLib.Client.Events.Services.MessageThrottler
{
    /// <inheritdoc />
    /// <summary>Class representing event args for OnClientThrottled.</summary>
    public class OnClientThrottledArgs : EventArgs
    {
        /// <summary>Event property representing reason why message was throttled.</summary>
        public Enums.ThrottleType ThrottleViolation;
        /// <summary>Event property representing message that failed to send due to throttling.</summary>
        public string Message;
    }
}
