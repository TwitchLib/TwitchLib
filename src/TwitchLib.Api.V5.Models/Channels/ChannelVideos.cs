using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Channels
{
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
