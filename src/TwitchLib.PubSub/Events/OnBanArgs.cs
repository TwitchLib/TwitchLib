using System;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Object representing the arguments for a ban event
    /// </summary>
    public class OnBanArgs : EventArgs
    {
        /// <summary>
        /// Property representing banned user id
        /// </summary>
        public string BannedUserId;
        /// <summary>
        /// Property representing banned username
        /// </summary>
        public string BannedUser;
        /// <summary>
        /// Property representing ban reason.
        /// </summary>
        public string BanReason;
        /// <summary>
        /// Property representing the moderator who banned user.
        /// </summary>
        public string BannedBy;
        /// <summary>
        /// Property representing the user id of the moderator that banned the user.
        /// </summary>
        public string BannedByUserId;

        /// <summary>
        /// The channel ID the event came from
        /// </summary>
        public string ChannelId;
    }
}
