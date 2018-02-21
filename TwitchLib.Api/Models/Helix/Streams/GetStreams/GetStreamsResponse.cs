using Newtonsoft.Json;

namespace TwitchLib.Api.Models.Helix.Streams.GetStreams
{
    public class GetStreamsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Stream[] Streams { get; protected set; }
        [JsonProperty(PropertyName = "pagination")]
        public Common.Pagination Pagination { get; protected set; }
    }
}
