using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Videos.GetVideos
{
    public class MutedSegment
    {
        [JsonPropertyName("duration")]
        public int Duration { get; protected set; }
        [JsonPropertyName("offset")]
        public int Offset { get; protected set; }
    }
}