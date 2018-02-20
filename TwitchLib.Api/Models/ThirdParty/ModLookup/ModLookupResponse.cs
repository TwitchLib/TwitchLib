using Newtonsoft.Json;

namespace TwitchLib.Api.Models.ThirdParty.ModLookup
{
    public class ModLookupResponse
    {
        [JsonProperty(PropertyName = "status")]
        public int Status { get; protected set; }
        [JsonProperty(PropertyName = "user")]
        public string User { get; protected set; }
        [JsonProperty(PropertyName = "count")]
        public int Count { get; protected set; }
        [JsonProperty(PropertyName = "channels")]
        public ModLookupListing[] Channels { get; protected set; }
    }
}
