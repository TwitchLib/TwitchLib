using Newtonsoft.Json;

namespace TwitchLib.Models.API.v3.Streams
{
    public class StreamResponse
    {
        [JsonProperty(PropertyName = "stream")]
        public Stream Stream;
    }
}
