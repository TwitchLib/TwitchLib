using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Videos
{
    public class FollowedVideos
    {
        #region Videos
        [JsonProperty(PropertyName = "videos")]
        public Video[] Videos { get; protected set; }
        #endregion
    }
}
