using Newtonsoft.Json;

namespace TwitchLib.Models.API.v3.Search
{
    public class SearchGamesResponse
    {
        [JsonProperty(PropertyName = "games")]
        public Games.Game[] Games { get; protected set; }
    }
}
