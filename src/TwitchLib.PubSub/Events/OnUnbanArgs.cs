using System;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// OnUnban event arguments class.
    /// </summary>
    public class OnUnbanArgs : EventArgs
    {
        /// <summary>
        /// Name of user that was unbanned
        /// </summary>
        public string UnbannedUser;
        /// <summary>
        /// Userid of user that was unbanned.
        /// </summary>
        public string UnbannedUserId;
        /// <summary>
        /// Name of moderator that issued unban command.
        /// </summary>
        public string UnbannedBy;
        /// <summary>
        /// Userid of the unbanned user.
        /// </summary>
        public string UnbannedByUserId;
        /// <summary>
        /// The channel ID the event came from
        /// </summary>
        public string ChannelId;
    }
}
