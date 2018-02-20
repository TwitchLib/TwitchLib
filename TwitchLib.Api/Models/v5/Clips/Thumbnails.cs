using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v5.Clips
{
    public class Thumbnails
    {
        [JsonProperty(PropertyName = "medium")]
        public string Medium { get; protected set; }
        [JsonProperty(PropertyName = "small")]
        public string Small { get; protected set; }
        [JsonProperty(PropertyName = "tiny")]
        public string Tiny { get; protected set; }
    }
}
