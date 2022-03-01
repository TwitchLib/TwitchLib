using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreams
{
    public class GetStreamsResponse
    {
        [JsonPropertyName("data")]
        public Stream[] Streams { get; protected set; }
        [JsonPropertyName("pagination")]
        public Common.Pagination Pagination { get; protected set; }
    }
}
