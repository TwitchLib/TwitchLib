namespace TwitchLib.EventSub.Core.SubscriptionTypes.Channel
{
    /// <summary>
    /// Channel Subscription End subscription type model
    /// <para>Description:</para>
    /// <para>A notification when a subscription to the specified channel ends.</para>
    /// </summary>
    public class ChannelSubscriptionEnd
    {
        /// <summary>
        /// The user ID for the user whose subscription ended.
        /// </summary>
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// The user login for the user whose subscription ended.
        /// </summary>
        public string UserLogin { get; set; } = string.Empty;
        /// <summary>
        /// The user display name for the user whose subscription ended.
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// The broadcaster user ID.
        /// </summary>
        public string BroadcasterUserId { get; set; } = string.Empty;
        /// <summary>
        /// The broadcaster login.
        /// </summary>
        public string BroadcasterUserLogin { get; set; } = string.Empty;
        /// <summary>
        /// The broadcaster display name.
        /// </summary>
        public string BroadcasterUserName { get; set; } = string.Empty;
        /// <summary>
        /// The tier of the subscription that ended. Valid values are 1000, 2000, and 3000.
        /// </summary>
        public string Tier { get; set; } = string.Empty;
        /// <summary>
        /// Whether the subscription was a gift.
        /// </summary>
        public bool IsGift { get; set; }
    }
}