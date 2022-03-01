using System.Text.Json.Serialization;
using System;

namespace TwitchLib.Api.Helix.Models.Schedule
{
    public class Segment
    {
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("start_time")]
        public DateTime StartTime { get; protected set; }
        [JsonPropertyName("end_time")]
        public DateTime EndTime { get; protected set; }
        [JsonPropertyName("title")]
        public string Title { get; protected set; }
        [JsonPropertyName("canceled_until")]
        public DateTime? CanceledUntil { get; protected set; }
        [JsonPropertyName("category")]
        public Category Category { get; protected set; }
        [JsonPropertyName("is_recurring")]
        public bool IsRecurring { get; protected set; }
    }
}