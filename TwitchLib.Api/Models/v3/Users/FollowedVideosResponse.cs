using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v3.Users
{
    public class FollowedVideosResponse
    {
        [JsonProperty(PropertyName = "videos")]
        public Videos.Video[] Videos { get; protected set; } 
    }
}
