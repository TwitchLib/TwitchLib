using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v3.Games
{
    public class TopGamesResponse
    {
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
        [JsonProperty(PropertyName = "top")]
        public TopGame[] TopGames { get; protected set; }
    }
}
