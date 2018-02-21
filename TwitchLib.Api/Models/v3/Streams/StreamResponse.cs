using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v3.Streams
{
    public class StreamResponse
    {
        [JsonProperty(PropertyName = "stream")]
        public Stream Stream;
    }
}
