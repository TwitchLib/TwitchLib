using TwitchLib.EventSub.Webhooks.Core.Models.Subscriptions;

namespace TwitchLib.EventSub.Webhooks.Core.SubscriptionTypes.Channel
{
    /// <summary>
    /// Channel Subscription Message subscription type model
    /// <para>Description:</para>
    /// <para>A notification when a user sends a resubscription chat message in a specific channel.</para>
    /// </summary>
    public class ChannelSubscriptionMessage
    {
        /// <summary>
        /// The user ID of the user who sent a resubscription chat message.
        /// </summary>
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// The user display name of the user who a resubscription chat message.
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// The user login of the user who sent a resubscription chat message.
        /// </summary>
        public string UserLogin { get; set; } = string.Empty;
        /// <summary>
        /// The broadcaster user ID.
        /// </summary>
        public string BroadcasterUserId { get; set; } = string.Empty;
        /// <summary>
        /// The broadcaster display name.
        /// </summary>
        public string BroadcasterUserName { get; set; } = string.Empty;
        /// <summary>
        /// The broadcaster login.
        /// </summary>
        public string BroadcasterUserLogin { get; set; } = string.Empty;
        /// <summary>
        /// The tier of the user's subscription. Valid values are 1000, 2000, and 3000.
        /// </summary>
        public string Tier { get; set; } = string.Empty;
        /// <summary>
        /// An object that contains the resubscription message and emote information needed to recreate the message.
        /// </summary>
        public SubscriptionMessage Message { get; set; } = new();
        /// <summary>
        /// The total number of months the user has been subscribed to the channel.
        /// </summary>
        public int CumulativeTotal { get; set; }
        /// <summary>
        /// The number of consecutive months the user’s current subscription has been active. This value is null if the user has opted out of sharing this information.
        /// </summary>
        public int? StreakMonths { get; set; }
        /// <summary>
        /// The month duration of the subscription.
        /// </summary>
        public int DurationMonths { get; set; }
    }
}