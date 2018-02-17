using Newtonsoft.Json;

namespace TwitchLib.Models.API.v5.Videos
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
