namespace TwitchLib.Models.API.v5.Videos
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class VideoThumbnails
    {
        #region Large
        [JsonProperty(PropertyName = "large")]
        public VideoThumbnail[] Large { get; internal set; }
        #endregion
        #region Medium
        [JsonProperty(PropertyName = "medium")]
        public VideoThumbnail[] Medium { get; internal set; }
        #endregion
        #region Small
        [JsonProperty(PropertyName = "small")]
        public VideoThumbnail[] Small { get; internal set; }
        #endregion
        #region Template
        [JsonProperty(PropertyName = "template")]
        public VideoThumbnail[] Template { get; internal set; }
        #endregion
    }
}
