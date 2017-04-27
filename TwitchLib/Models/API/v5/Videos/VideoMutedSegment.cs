namespace TwitchLib.Models.API.v5.Videos
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
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
