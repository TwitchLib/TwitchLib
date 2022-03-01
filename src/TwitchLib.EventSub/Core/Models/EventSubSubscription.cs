using System;
using System.Collections.Generic;

namespace TwitchLib.EventSub.Core.Models
{
    /// <summary>
    /// Defines an EventSub Subscription
    /// </summary>
    public class EventSubSubscription
    {
        /// <summary>
        /// The ID of the subscription
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// The subscription type name.
        /// </summary>
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// The subscription type version.
        /// </summary>
        public string Version { get; set; } = string.Empty;
        /// <summary>
        /// The status of the subscription.
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Subscription-specific parameters. The parameters inside this object differ by subscription type and may differ by version.
        /// </summary>
        public Dictionary<string, string> Condition { get; set; } = new();
        /// <summary>
        /// Transport-specific parameters.
        /// </summary>
        public EventSubSubscriptionTransport Transport { get; set; } = new();
        /// <summary>
        /// Whether batching is enabled for the subscription.
        /// <para>!! Currently only supported for drop.entitlement.grant !!</para>
        /// </summary>
        public bool IsBatchingEnabled { get; set; }
        /// <summary>
        /// The time the subscription was created.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.MinValue;
        /// <summary>
        /// How much the subscription counts against your limit. 
        /// </summary>
        public int Cost { get; set; }
    }
}