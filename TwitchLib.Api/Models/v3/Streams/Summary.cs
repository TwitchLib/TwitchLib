using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v3.Streams
{
    public class Summary
    {
        [JsonProperty(PropertyName = "viewers")]
        public int Viewers { get; protected set; }
        [JsonProperty(PropertyName = "channels")]
        public int Channels { get; protected set; }
    }
}
