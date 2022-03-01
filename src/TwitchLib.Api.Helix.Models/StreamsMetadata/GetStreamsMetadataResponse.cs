using System.Text.Json.Serialization;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.StreamsMetadata
{
    public class GetStreamsMetadataResponse
    {
        [JsonPropertyName("data")]
        public StreamMetadata[] StreamsMetadatas { get; protected set; }
        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
