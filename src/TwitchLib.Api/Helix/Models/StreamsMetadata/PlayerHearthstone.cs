using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.StreamsMetadata
{
    public class PlayerHearthstone
    {
        [JsonPropertyName("hero")]
        public HeroHearthstone Hero { get; protected set; }
    }
}
