using System;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Untimeout argument class.
    /// </summary>
    public class OnUntimeoutArgs : EventArgs
    {
        /// <summary>
        /// User that was untimed out (ie unbanned for a timeout)
        /// </summary>
        public string UntimeoutedUser;
        /// <summary>
        /// Userid that was untimed out (ie unbanned for a timeout)
        /// </summary>
        public string UntimeoutedUserId;
        /// <summary>
        /// Moderator that issued the untimeout command.
        /// </summary>
        public string UntimeoutedBy;
        /// <summary>
        /// Moderator user id that issued untimeout command.
        /// </summary>
        public string UntimeoutedByUserId;
        /// <summary>
        /// The channel ID the event came from
        /// </summary>
        public string ChannelId;
    }
}
