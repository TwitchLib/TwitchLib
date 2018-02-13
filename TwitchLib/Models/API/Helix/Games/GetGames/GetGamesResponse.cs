using Newtonsoft.Json;

namespace TwitchLib.Models.API.Helix.Games.GetGames
{
    public class GetGamesResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Game[] Games { get; protected set; }
    }
}
