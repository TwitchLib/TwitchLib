using Newtonsoft.Json;
using TwitchLib.Api.Models.Helix.Common;

namespace TwitchLib.Api.Models.Helix.Videos.GetVideos
{
    public class GetVideosResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Video[] Videos { get; protected set; }
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
