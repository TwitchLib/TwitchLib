using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Bits
{
    public class Images
    {
        [JsonProperty(PropertyName = "dark")]
        public DarkImage Dark { get; set; }
        [JsonProperty(PropertyName = "light")]
        public LightImage Light { get; set; }
    }
}
