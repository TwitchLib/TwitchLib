using Newtonsoft.Json;

namespace TwitchLib.Models.API.v5.Bits
{
    public class DarkImage
    {
        [JsonProperty(PropertyName = "animated")]
        public ImageLinks Animated { get; set; }
        [JsonProperty(PropertyName = "static")]
        public ImageLinks Static { get; set; }
    }
}
