namespace TwitchLib.Models.API.v5.Channels
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class ChannelVideos
    {
        #region Total
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
        #endregion
        #region Videos
        [JsonProperty(PropertyName = "videos")]
        public Videos.Video[] Videos { get; protected set; }
        #endregion
    }
}
