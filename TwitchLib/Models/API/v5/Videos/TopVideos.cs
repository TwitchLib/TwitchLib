namespace TwitchLib.Models.API.v5.Videos
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class TopVideos
    {
        #region VODs
        [JsonProperty(PropertyName = "vods")]
        public Video[] VODs { get; protected set; }
        #endregion
    }
}
