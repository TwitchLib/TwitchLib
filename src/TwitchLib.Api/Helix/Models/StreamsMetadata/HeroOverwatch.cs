using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.StreamsMetadata
{
    public class HeroOverwatch
    {
        [JsonPropertyName("ability")]
        public string Ability { get; protected set; }
        [JsonPropertyName("name")]
        public string Name { get; protected set; }
        [JsonPropertyName("role")]
        public string Role { get; protected set; }
    }
}
