using Newtonsoft.Json;
using TwitchLib.Api.Models.Helix.Common;

namespace TwitchLib.Api.Models.Helix.StreamsMetadata
{
    public class GetStreamsMetadataResponse
    {
        [JsonProperty(PropertyName = "data")]
        public StreamMetadata[] StreamsMetadatas { get; protected set; }
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
