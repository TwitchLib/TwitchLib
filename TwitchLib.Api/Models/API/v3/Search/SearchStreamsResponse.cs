using Newtonsoft.Json;

namespace TwitchLib.Models.API.v3.Search
{
    public class SearchStreamsResponse
    {
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
        [JsonProperty(PropertyName = "streams")]
        public Streams.Stream[] Streams { get; protected set; }
    }
}
