using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Videos
{
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
