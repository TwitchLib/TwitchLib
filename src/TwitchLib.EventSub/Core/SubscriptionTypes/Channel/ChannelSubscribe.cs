namespace TwitchLib.EventSub.Core.SubscriptionTypes.Channel
{
    /// <summary>
    /// Channel Subscribe subscription type model
    /// <para>Description:</para>
    /// <para>A notification when a specified channel receives a subscriber. This does not include resubscribes.</para>
    /// </summary>
    public class ChannelSubscribe
    {
        /// <summary>
        /// The user ID for the user who subscribed to the specified channel.
        /// </summary>
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// The user display name for the user who subscribed to the specified channel.
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// The user login for the user who subscribed to the specified channel.
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
        /// The tier of the subscription. Valid values are 1000, 2000, and 3000.
        /// </summary>
        public string Tier { get; set; } = string.Empty;
        /// <summary>
        /// Whether the subscription is a gift.
        /// </summary>
        public bool IsGift { get; set; }
    }
}