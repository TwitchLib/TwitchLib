namespace TwitchLib.Models.API.v5.Videos
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class FollowedVideos
    {
        #region Videos
        [JsonProperty(PropertyName = "videos")]
        public Video[] Videos { get; protected set; }
        #endregion
    }
}
