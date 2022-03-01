using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Videos
{
    public class TopVideos
    {
        #region VODs
        [JsonProperty(PropertyName = "vods")]
        public Video[] VODs { get; protected set; }
        #endregion
    }
}
