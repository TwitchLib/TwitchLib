using Newtonsoft.Json;

namespace TwitchLib.Models.API.ThirdParty.ModLookup
{
    public class StatsResponse
    {
        [JsonProperty(PropertyName = "status")]
        public int Status { get; protected set; }
        [JsonProperty(PropertyName = "stats")]
        public Stats Stats { get; protected set; }
    }
}
