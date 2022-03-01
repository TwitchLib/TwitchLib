using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Search
{
    public class SearchGames
    {
        #region Games
        [JsonProperty(PropertyName = "games")]
        public Games.Game[] Games { get; protected set; }
        #endregion
    }
}
