using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v5.Bits
{
    public class Images
    {
        [JsonProperty(PropertyName = "dark")]
        public DarkImage Dark { get; set; }
        [JsonProperty(PropertyName = "light")]
        public LightImage Light { get; set; }
    }
}
