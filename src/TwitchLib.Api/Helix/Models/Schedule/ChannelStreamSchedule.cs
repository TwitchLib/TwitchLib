using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Schedule
{
    public class ChannelStreamSchedule
    {
        [JsonPropertyName("segments")]
        public Segment[] Segments { get; protected set; }
        [JsonPropertyName("broadcaster_id")]
        public string BroadcasterId { get; protected set; }
        [JsonPropertyName("broadcaster_name")]
        public string BroadcasterName { get; protected set; }
        [JsonPropertyName("broadcaster_login")]
        public string BroadcasterLogin { get; protected set; }
        [JsonPropertyName("vacation")]
        public Vacation Vacation { get; protected set; }
    }
}