using System;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Class OnFollowArgs.
    /// </summary>
    public class OnFollowArgs : EventArgs
    {
        /// <summary>
        /// The followed channel identifier
        /// </summary>
        public string FollowedChannelId;
        /// <summary>
        /// The display name
        /// </summary>
        public string DisplayName;
        /// <summary>
        /// The username
        /// </summary>
        public string Username;
        /// <summary>
        /// The user identifier
        /// </summary>
        public string UserId;
    }
}
