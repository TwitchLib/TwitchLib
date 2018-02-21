using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v3.Videos
{
    public class TopVideosResponse
    {
        [JsonProperty(PropertyName = "videos")]
        public Video[] TopVideos { get; protected set; }
    }
}
