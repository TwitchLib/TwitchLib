using Newtonsoft.Json;

namespace TwitchLib.Models.API.v3.Videos
{
    public class TopVideosResponse
    {
        [JsonProperty(PropertyName = "videos")]
        public Video[] TopVideos { get; protected set; }
    }
}
