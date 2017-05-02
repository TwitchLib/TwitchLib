namespace TwitchLib.Models.API.v5.Videos
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class VideoThumbnail
    {
        #region Type
        [JsonProperty(PropertyName = "type")]
        public string Type { get; protected set; }
        #endregion
        #region Url
        [JsonProperty(PropertyName = "url")]
        public string Url { get; protected set; }
        #endregion
    }
}
