namespace TwitchLib.Models.API.v5.Search
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class SearchChannels
    {
        #region Total
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
        #endregion
        #region Channels
        [JsonProperty(PropertyName = "channels")]
        public Channels.Channel[] Channels { get; protected set; }
        #endregion
    }
}
