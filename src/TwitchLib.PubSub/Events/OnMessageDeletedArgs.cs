using System;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// OnMessageDeleted event arguments class.
    /// </summary>
    public class OnMessageDeletedArgs : EventArgs
    {
        /// <summary>
        /// Name of the user whose message was deleted
        /// </summary>
        public string TargetUser;

        /// <summary>
        /// ID of the user whose message was deleted
        /// </summary>
        public string TargetUserId;

        /// <summary>
        /// Name of the moderator who deleted the message
        /// </summary>
        public string DeletedBy;

        /// <summary>
        /// ID of the moderator who deleted the message
        /// </summary>
        public string DeletedByUserId;

        /// <summary>
        /// The message that was deleted
        /// </summary>
        public string Message;

        /// <summary>
        /// ID of the message that was deleted
        /// </summary>
        public string MessageId;

        /// <summary>
        /// The channel ID the event came from
        /// </summary>
        public string ChannelId;
    }
}
