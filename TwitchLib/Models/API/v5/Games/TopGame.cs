namespace TwitchLib.Models.API.v5.Games
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class TopGame
    {
        #region Channels
        [JsonProperty(PropertyName = "channels")]
        public int Channels { get; protected set; }
        #endregion
        #region Viewers
        [JsonProperty(PropertyName = "viewers")]
        public int Viewers { get; protected set; }
        #endregion
        #region Game
        [JsonProperty(PropertyName = "game")]
        public Game Game { get; protected set; }
        #endregion
    }
}
