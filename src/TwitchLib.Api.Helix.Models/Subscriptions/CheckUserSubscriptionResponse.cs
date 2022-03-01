using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Subscriptions
{
    public class CheckUserSubscriptionResponse
    {
        [JsonPropertyName("data")]
        public Subscription[] Data { get; protected set; }
    }
}