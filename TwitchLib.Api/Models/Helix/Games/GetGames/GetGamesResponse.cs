using Newtonsoft.Json;

namespace TwitchLib.Api.Models.Helix.Games.GetGames
{
    public class GetGamesResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Game[] Games { get; protected set; }
    }
}
