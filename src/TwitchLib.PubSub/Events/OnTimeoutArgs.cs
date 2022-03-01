using System;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Class representing timeout event.
    /// </summary>
    public class OnTimeoutArgs : EventArgs
    {
        /// <summary>
        /// Property representing the timedout user id.
        /// </summary>
        public string TimedoutUserId;
        /// <summary>
        /// Property representing the timedout username.
        /// </summary>
        public string TimedoutUser;
        /// <summary>
        /// Property representing the tumeout duration.
        /// </summary>
        public TimeSpan TimeoutDuration;
        /// <summary>
        /// Property representing the timeout reaosn.
        /// </summary>
        public string TimeoutReason;
        /// <summary>
        /// Property representing the moderator that issued the command.
        /// </summary>
        public string TimedoutBy;
        /// <summary>
        /// Property representing the moderator that issued the command's user id.
        /// </summary>
        public string TimedoutById;

        /// <summary>
        /// The channel ID the event came from
        /// </summary>
        public string ChannelId;
    }
}
