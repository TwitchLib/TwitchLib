namespace TwitchLib.Events.PubSub
{
    #region using directives
    using System;
    #endregion
    /// <summary>Class representing timeout event.</summary>
    public class OnTimeoutArgs
    {
        /// <summary>Property representing the timedout user.</summary>
        public string TimedoutUserId;
        /// <summary>Property representing the tumeout duration.</summary>
        public TimeSpan TimeoutDuration;
        /// <summary>Property representing the timeout reaosn.</summary>
        public string TimeoutReason;
        /// <summary>Property representing the moderator that issued the command.</summary>
        public string TimedoutBy;
        /// <summary>Property representing the moderator that issued the command's user id.</summary>
        public string TimedoutById;
    }
}
