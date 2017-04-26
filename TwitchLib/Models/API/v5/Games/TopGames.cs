namespace TwitchLib.Models.API.v5.Games
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class TopGames
    {
        #region Total
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
        #endregion
        #region Top
        [JsonProperty(PropertyName = "top")]
        public TopGame[] Top { get; protected set; }
        #endregion
    }
}
