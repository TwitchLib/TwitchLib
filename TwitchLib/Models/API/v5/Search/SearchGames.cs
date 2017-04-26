namespace TwitchLib.Models.API.v5.Search
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class SearchGames
    {
        #region Games
        [JsonProperty(PropertyName = "games")]
        public Games.Game[] Games { get; protected set; }
        #endregion
    }
}
