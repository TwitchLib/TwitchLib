using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.HypeTrain
{
    public class HypeTrain
    {
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("event_type")]
        public string EventType { get; protected set; }
        [JsonPropertyName("event_timestamp")]
        public string EventTimeStamp { get; protected set; }
        [JsonPropertyName("version")]
        public string Version { get; protected set; }
        [JsonPropertyName("event_data")]
        public HypeTrainEventData EventData { get; protected set; }
    }
}