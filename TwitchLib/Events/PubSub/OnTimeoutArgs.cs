using System;

namespace TwitchLib.Events.PubSub
{
    /// <summary>Class representing timeout event.</summary>
    public class OnTimeoutArgs
    {
        /// <summary>Property representing the timedout user.</summary>
        public string TimedoutUser;
        /// <summary>Property representing the tumeout duration.</summary>
        public TimeSpan TimeoutDuration;
        /// <summary>Property representing the timeout reaosn.</summary>
        public string TimeoutReason;
        /// <summary>Property representing the moderator that issued the command.</summary>
        public string TimedoutBy;
    }
}
