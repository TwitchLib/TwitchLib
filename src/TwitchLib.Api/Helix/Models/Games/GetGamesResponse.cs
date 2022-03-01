using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Games
{
    public class GetGamesResponse
    {
        [JsonPropertyName("data")]
        public Game[] Games { get; protected set; }
    }
}
