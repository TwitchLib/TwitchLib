using Newtonsoft.Json;

namespace TwitchLib.Models.API.v5.Videos
{
    public class FollowedVideos
    {
        #region Videos
        [JsonProperty(PropertyName = "videos")]
        public Video[] Videos { get; protected set; }
        #endregion
    }
}
