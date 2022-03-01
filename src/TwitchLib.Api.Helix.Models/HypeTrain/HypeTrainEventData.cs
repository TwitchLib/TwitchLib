using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.HypeTrain
{
    public class HypeTrainEventData
    {
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("broadcaster_id")]
        public string BroadcasterId { get; protected set; }
        [JsonPropertyName("started_at")]
        public string StartedAt { get; protected set; }
        [JsonPropertyName("expires_at")]
        public string ExpiresAt { get; protected set; }
        [JsonPropertyName("cooldown_end_time")]
        public string CooldownEndTime { get; protected set; }
        [JsonPropertyName("level")]
        public int Level { get; protected set; }
        [JsonPropertyName("goal")]
        public int Goal { get; protected set; }
        [JsonPropertyName("total")]
        public int Total { get; protected set; }
        [JsonPropertyName("top_contribution")]
        public HypeTrainContribution TopContribution { get; protected set; }
        [JsonPropertyName("last_contribution")]
        public HypeTrainContribution LastContribution { get; protected set; }
    }
}