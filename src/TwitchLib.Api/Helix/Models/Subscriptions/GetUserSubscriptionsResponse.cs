using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Subscriptions
{
    public class GetUserSubscriptionsResponse
    {
        [JsonPropertyName("data")]
        public Subscription[] Data { get; protected set; }
    }
}