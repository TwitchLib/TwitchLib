using Newtonsoft.Json;

namespace TwitchLib.Api.Models.ThirdParty.ModLookup
{
    public class TopResponse
    {
        [JsonProperty(PropertyName = "status")]
        public int Status { get; protected set; }
        [JsonProperty(PropertyName = "top")]
        public Top Top { get; protected set; }
    }
}
