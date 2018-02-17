using Newtonsoft.Json;

namespace TwitchLib.Models.API.v3.Streams
{
    public class StreamsResponse
    {
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
        [JsonProperty(PropertyName = "streams")]
        public Stream[] Streams { get; protected set; }
    }
}
