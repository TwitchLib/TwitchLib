using Newtonsoft.Json;

namespace TwitchLib.Models.API.v5.Search
{
    public class SearchGames
    {
        #region Games
        [JsonProperty(PropertyName = "games")]
        public Games.Game[] Games { get; protected set; }
        #endregion
    }
}
