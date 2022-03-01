using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Videos
{
    public class VideoMutedSegment
    {
        #region Duration
        [JsonProperty(PropertyName = "duration")]
        public long Duration { get; internal set; }
        #endregion
        #region Offset
        [JsonProperty(PropertyName = "offset")]
        public long Offset { get; internal set; }
        #endregion
    }
}
