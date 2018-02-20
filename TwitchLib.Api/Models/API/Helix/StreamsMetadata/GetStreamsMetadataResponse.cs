using Newtonsoft.Json;
using TwitchLib.Models.API.Helix.Common;

namespace TwitchLib.Models.API.Helix.StreamsMetadata
{
    public class GetStreamsMetadataResponse
    {
        [JsonProperty(PropertyName = "data")]
        public StreamMetadata[] StreamsMetadatas { get; protected set; }
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
