using Newtonsoft.Json;

namespace TwitchLib.Models.API.v3.Users
{
    public class FollowedVideosResponse
    {
        [JsonProperty(PropertyName = "videos")]
        public Videos.Video[] Videos { get; protected set; } 
    }
}
