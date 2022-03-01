using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.StreamsMetadata
{
    public class HeroHearthstone
    {
        [JsonPropertyName("class")]
        public string Class { get; protected set; }
        [JsonPropertyName("name")]
        public string Name { get; protected set; }
        [JsonPropertyName("type")]
        public string Type { get; protected set; }
    }
}
