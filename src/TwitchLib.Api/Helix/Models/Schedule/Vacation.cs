using System.Text.Json.Serialization;
using System;

namespace TwitchLib.Api.Helix.Models.Schedule
{
    public class Vacation
    {
        [JsonPropertyName("start_time")]
        public DateTime StartTime { get; protected set; }
        [JsonPropertyName("end_time")]
        public DateTime EndTime { get; protected set; }
    }
}