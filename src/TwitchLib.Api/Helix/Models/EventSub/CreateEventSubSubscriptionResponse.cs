using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.EventSub
{
    public class CreateEventSubSubscriptionResponse
    {
        [JsonPropertyName("data")]
        public EventSubSubscription[] Subscriptions { get; protected set; }
        [JsonPropertyName("total")]
        public int Total { get; protected set; }
        [JsonPropertyName("total_cost")]
        public int TotalCost { get; protected set; }
        [JsonPropertyName("max_total_cost")]
        public int MaxTotalCost { get; protected set; }
    }
}