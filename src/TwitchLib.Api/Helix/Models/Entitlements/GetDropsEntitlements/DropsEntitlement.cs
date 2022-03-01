using System.Text.Json.Serialization;
using System;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Helix.Models.Entitlements.GetDropsEntitlements
{
    public class DropsEntitlement
    {
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("benefit_id")]
        public string BenefitId { get; protected set; }
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; protected set; }
        [JsonPropertyName("user_id")]
        public string UserId { get; protected set; }
        [JsonPropertyName("game_id")]
        public string GameId { get; protected set; }
        [JsonPropertyName("fulfillment_status")]
        public FulfillmentStatus FulfillmentStatus { get; protected set; }
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; protected set; }
    }
}
