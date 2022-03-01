using System.Text.Json.Serialization;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.EventSub
{
    public class GetEventSubSubscriptionsResponse
    {
        [JsonPropertyName("total")]
        public int Total { get; protected set; }
        [JsonPropertyName("data")]
        public EventSubSubscription[] Subscriptions { get; protected set; }
        [JsonPropertyName("total_cost")]
        public int TotalCost { get; protected set; }
        [JsonPropertyName("max_total_cost")]
        public int MaxTotalCost { get; protected set; }
        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; protected set; }
    }
}