using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v5.Videos
{
    public class TopVideos
    {
        #region VODs
        [JsonProperty(PropertyName = "vods")]
        public Video[] VODs { get; protected set; }
        #endregion
    }
}
