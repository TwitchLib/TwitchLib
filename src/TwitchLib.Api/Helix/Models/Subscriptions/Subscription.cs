using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Subscriptions
{
    public class Subscription
    {
        [JsonPropertyName("broadcaster_id")]
        public string BroadcasterId { get; protected set; }
        [JsonPropertyName("broadcaster_name")]
        public string BroadcasterName { get; protected set; }
        [JsonPropertyName("broadcaster_login")]
        public string BroadcasterLogin { get; protected set; }
        [JsonPropertyName("is_gift")]
        public bool IsGift { get; protected set; }
        [JsonPropertyName("tier")]
        public string Tier { get; protected set; }
        [JsonPropertyName("plan_name")]
        public string PlanName { get; protected set; }
        [JsonPropertyName("user_id")]
        public string UserId { get; protected set; }
        [JsonPropertyName("user_name")]
        public string UserName { get; protected set; }
        [JsonPropertyName("gifter_id")]
        public string GiftertId { get; protected set; }
        [JsonPropertyName("gifter_name")]
        public string GifterName { get; protected set; }
        [JsonPropertyName("gifter_login")]
        public string GifterLogin { get; protected set; }
    }
}