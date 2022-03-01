namespace TwitchLib.EventSub.Core.SubscriptionTypes.Channel
{
    /// <summary>
    /// Channel Cheer subscription type model
    /// <para>Description:</para>
    /// <para>A user cheers on the specified channel.</para>
    /// </summary>
    public class ChannelCheer
    {
        /// <summary>
        /// Whether the user cheered anonymously or not.
        /// </summary>
        public bool IsAnonymous { get; set; }
        /// <summary>
        /// The user ID for the user who cheered on the specified channel. This is null if is_anonymous is true.
        /// </summary>
        public string? UserId { get; set; }
        /// <summary>
        /// The user display name for the user who cheered on the specified channel. This is null if is_anonymous is true.
        /// </summary>
        public string? UserName { get; set; }
        /// <summary>
        /// The user login for the user who cheered on the specified channel. This is null if is_anonymous is true.
        /// </summary>
        public string? UserLogin { get; set; }
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
        /// The message sent with the cheer.
        /// </summary>
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// The number of bits cheered.
        /// </summary>
        public int Bits { get; set; }
    }
}