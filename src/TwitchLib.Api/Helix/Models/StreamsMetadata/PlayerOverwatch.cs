using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.StreamsMetadata
{
    public class PlayerOverwatch
    {
        [JsonPropertyName("hero")]
        public HeroOverwatch Hero { get; protected set; }
    }
}
