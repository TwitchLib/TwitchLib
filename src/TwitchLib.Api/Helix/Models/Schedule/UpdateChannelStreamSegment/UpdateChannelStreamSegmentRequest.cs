using System.Text.Json.Serialization;
using System;

namespace TwitchLib.Api.Helix.Models.Schedule.UpdateChannelStreamSegment
{
    public class UpdateChannelStreamSegmentRequest
    {
        [JsonPropertyName("start_time")]
        public DateTime StartTime { get; set; }
        [JsonPropertyName("duration")]
        public string Duration { get; set; }
        [JsonPropertyName("category_id")]
        public string CategoryId { get; set; }
        [JsonPropertyName("is_canceled")]
        public bool IsCanceled { get; set; }
        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }
    }
}