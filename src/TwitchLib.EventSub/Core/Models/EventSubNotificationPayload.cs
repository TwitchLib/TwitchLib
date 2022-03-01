namespace TwitchLib.EventSub.Core.Models
{
    /// <summary>
    /// Defines a notification payload of an EventSub notification
    /// </summary>
    /// <typeparam name="T">SubscriptionType</typeparam>
    public class EventSubNotificationPayload<T> where T : new()
    {
        /// <summary>
        /// Data about the subscription the notifications belong to
        /// </summary>
        public EventSubSubscription Subscription { get; set; } = new();
        /// <summary>
        /// Event Data
        /// </summary>
        public T Event { get; set; } = new();
    }
}