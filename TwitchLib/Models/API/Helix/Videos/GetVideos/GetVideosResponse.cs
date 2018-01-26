using Newtonsoft.Json;
using TwitchLib.Models.API.Helix.Common;

namespace TwitchLib.Models.API.Helix.Videos.GetVideos
{
    public class GetVideosResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Video[] Videos { get; protected set; }
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
