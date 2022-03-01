using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Streams.CreateStreamMarker
{
    public class CreateStreamMarkerRequest
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
