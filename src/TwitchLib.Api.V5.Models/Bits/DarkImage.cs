using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Bits
{
    public class DarkImage
    {
        [JsonProperty(PropertyName = "animated")]
        public ImageLinks Animated { get; set; }
        [JsonProperty(PropertyName = "static")]
        public ImageLinks Static { get; set; }
    }
}
