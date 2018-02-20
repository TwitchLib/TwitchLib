using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v3.Search
{
    public class SearchGamesResponse
    {
        [JsonProperty(PropertyName = "games")]
        public Games.Game[] Games { get; protected set; }
    }
}
