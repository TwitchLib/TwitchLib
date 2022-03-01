using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Games
{
    public class Game
    {
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("name")]
        public string Name { get; protected set; }
        [JsonPropertyName("box_art_url")]
        public string BoxArtUrl { get; protected set; }
    }
}
