namespace TwitchLib.EventSub.Core.SubscriptionTypes.Channel
{
    /// <summary>
    /// Channel Unban subscription type model
    /// <para>Description:</para>
    /// <para>A viewer is unbanned from the specified channel.</para>
    /// </summary>
    public class ChannelUnban
    {
        /// <summary>
        /// The user id for the user who was unbanned on the specified channel.
        /// </summary>
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// The user display name for the user who was unbanned on the specified channel.
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// The user login for the user who was unbanned on the specified channel.
        /// </summary>
        public string UserLogin { get; set; } = string.Empty;
        /// <summary>
        /// The requested broadcaster ID.
        /// </summary>
        public string BroadcasterUserId { get; set; } = string.Empty;
        /// <summary>
        /// The requested broadcaster display name.
        /// </summary>
        public string BroadcasterUserName { get; set; } = string.Empty;
        /// <summary>
        /// The requested broadcaster login.
        /// </summary>
        public string BroadcasterUserLogin { get; set; } = string.Empty;
        /// <summary>
        /// The user ID of the issuer of the unban.
        /// </summary>
        public string ModeratorUserId { get; set; } = string.Empty;
        /// <summary>
        /// The display name of the issuer of the unban.
        /// </summary>
        public string ModeratorUserName { get; set; } = string.Empty;
        /// <summary>
        /// The user login of the issuer of the unban.
        /// </summary>
        public string ModeratorUserLogin { get; set; } = string.Empty;
    }
}