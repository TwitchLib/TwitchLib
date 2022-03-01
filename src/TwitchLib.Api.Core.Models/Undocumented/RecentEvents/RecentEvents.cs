using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.RecentEvents
{
    public class RecentEvents
    {
        [JsonPropertyName("recent")]
        public Recent Recent { get; protected set; }
        [JsonPropertyName("top")]
        public Top Top { get; protected set; }
        [JsonPropertyName("has_recent_event")]
        public bool HasRecentEvent { get; protected set; }
        [JsonPropertyName("message_id")]
        public string MessageId { get; protected set; }
        [JsonPropertyName("timestamp")]
        public string Timestamp { get; protected set; }
        [JsonPropertyName("channel_id")]
        public string ChannelId { get; protected set; }
        [JsonPropertyName("allotted_time_ms")]
        public string AllottedTimeMs { get; protected set; }
        [JsonPropertyName("time_remaining_ms")]
        public string TimeRemainingMs { get; protected set; }
        [JsonPropertyName("amount")]
        public int Amount { get; protected set; }
        [JsonPropertyName("bits_used")]
        public int? BitsUsed { get; protected set; }
        [JsonPropertyName("message")]
        public string Message { get; protected set; }
        [JsonPropertyName("user_id")]
        public string UserId { get; protected set; }
        [JsonPropertyName("username")]
        public string Username { get; protected set; }
        // TODO: consider tags property
    }
}
