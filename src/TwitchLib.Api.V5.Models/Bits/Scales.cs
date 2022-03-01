using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Bits
{
    public class Scales
    {
        [JsonProperty(PropertyName = "0")]
        public string Zero { get; set; }
        [JsonProperty(PropertyName = "1")]
        public string One { get; set; }
        [JsonProperty(PropertyName = "2")]
        public string Two { get; set; }
        [JsonProperty(PropertyName = "3")]
        public string Three { get; set; }
        [JsonProperty(PropertyName = "4")]
        public string Four { get; set; }
    }
}
