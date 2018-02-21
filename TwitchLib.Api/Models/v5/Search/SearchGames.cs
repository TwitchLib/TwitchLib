using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v5.Search
{
    public class SearchGames
    {
        #region Games
        [JsonProperty(PropertyName = "games")]
        public Games.Game[] Games { get; protected set; }
        #endregion
    }
}
