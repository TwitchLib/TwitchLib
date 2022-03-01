using System.Text.Json.Serialization;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Subscriptions
{
    public class GetBroadcasterSubscriptionsResponse
    {
        [JsonPropertyName("data")]
        public Subscription[] Data { get; protected set; }
        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; protected set; }
        [JsonPropertyName("total")]
        public int Total { get; protected set; }
        [JsonPropertyName("points")]
        public int Points { get; protected set; }
    }
}