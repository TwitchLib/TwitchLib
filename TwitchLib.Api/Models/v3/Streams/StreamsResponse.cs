using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v3.Streams
{
    public class StreamsResponse
    {
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
        [JsonProperty(PropertyName = "streams")]
        public Stream[] Streams { get; protected set; }
    }
}
