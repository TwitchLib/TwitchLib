using System;

namespace TwitchLib.EventSub.Webhooks.Core.Models
{
    /// <summary>
    /// Defines a notification payload of batched EventSub notifications
    /// </summary>
    /// <typeparam name="T">SubscriptionType</typeparam>
    public class BatchedNotificationPayload<T> where T: new()
    {
        /// <summary>
        /// Data about the subscription the notifications belong to
        /// </summary>
        public EventSubSubscription Subscription { get; set; } = new();
        /// <summary>
        /// Event data of the notifications
        /// </summary>
        public EventSubBatchedEvent<T>[] Events { get; set; } = Array.Empty<EventSubBatchedEvent<T>>();
    }
}