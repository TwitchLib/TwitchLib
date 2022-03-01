using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace TwitchLib.Api.Helix.Models.EventSub
{
    public class EventSubSubscription
    {
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("status")]
        public string Status { get; protected set; }
        [JsonPropertyName("type")]
        public string Type { get; protected set; }
        [JsonPropertyName("version")]
        public string Version { get; protected set; }
        [JsonPropertyName("condition")]
        public Dictionary<string, string> Condition { get; protected set; }
        [JsonPropertyName("created_at")]
        public string CreatedAt { get; protected set; }
        [JsonPropertyName("transport")]
        public EventSubTransport Transport { get; protected set; }
        [JsonPropertyName("cost")]
        public int Cost { get; protected set; }
    }
}
