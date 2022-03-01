using System.Text.Json.Serialization;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Videos.GetVideos
{
    public class GetVideosResponse
    {
        [JsonPropertyName("data")]
        public Video[] Videos { get; protected set; }
        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
