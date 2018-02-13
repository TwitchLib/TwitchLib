using Newtonsoft.Json;

namespace TwitchLib.Models.API.ThirdParty.ModLookup
{
    public class ModLookupListing
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }
        [JsonProperty(PropertyName = "followers")]
        public int Followers { get; protected set; }
        [JsonProperty(PropertyName = "views")]
        public int Views { get; protected set; }
        [JsonProperty(PropertyName = "partnered")]
        public bool Partnered { get; protected set; }
    }
}
