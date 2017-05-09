namespace TwitchLib.Models.API.v5.Games
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class GameBox
    {
        #region Large
        [JsonProperty(PropertyName = "large")]
        public string Large { get; protected set; }
        #endregion
        #region Medium
        [JsonProperty(PropertyName = "medium")]
        public string Medium { get; protected set; }
        #endregion
        #region Small
        [JsonProperty(PropertyName = "small")]
        public string Small { get; protected set; }
        #endregion
        #region Template
        [JsonProperty(PropertyName = "template")]
        public string Template { get; protected set; }
        #endregion
    }
}
