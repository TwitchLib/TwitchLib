using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.CSStreams
{
    public class CSStream
    {
        [JsonPropertyName("_id")]
        public string Id { get; protected set; }
        [JsonPropertyName("game")]
        public string Game { get; protected set; }
        [JsonPropertyName("viewers")]
        public int Viewers { get; protected set; }
        [JsonPropertyName("map")]
        public string Map { get; protected set; }
        [JsonPropertyName("map_name")]
        public string MapName { get; protected set; }
        [JsonPropertyName("map_img")]
        public string MapImg { get; protected set; }
        [JsonPropertyName("skill")]
        public int Skill { get; protected set; }
        [JsonPropertyName("preview")]
        public Preview Preview { get; protected set; }
        [JsonPropertyName("is_playlist")]
        public bool IsPlaylist { get; protected set; }
        [JsonPropertyName("user")]
        public User User { get; protected set; }
    }
}
