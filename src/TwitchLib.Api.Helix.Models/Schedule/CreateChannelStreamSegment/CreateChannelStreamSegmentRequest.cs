using System.Text.Json.Serialization;
using System;

namespace TwitchLib.Api.Helix.Models.Schedule.CreateChannelStreamSegment
{
    public class CreateChannelStreamSegmentRequest
    {
        // required
        [JsonPropertyName("start_time")]
        public DateTime StartTime { get; set; }
        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }
        [JsonPropertyName("is_recurring")]
        public bool IsRecurring { get; set; }
        // optional
        [JsonPropertyName("duration")]
        public string Duration { get; set; }
        [JsonPropertyName("category_id")]
        public string CategoryId { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
}