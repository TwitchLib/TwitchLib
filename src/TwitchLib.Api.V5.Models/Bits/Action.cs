using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Bits
{
    public class Action
    {
        [JsonProperty(PropertyName = "prefix")]
        public string Prefix { get; set; }
        [JsonProperty(PropertyName = "scales")]
        public string[] Scales { get; set; }
        [JsonProperty(PropertyName = "tiers")]
        public Tier[] Tiers { get; set; }
        [JsonProperty(PropertyName = "backgrounds")]
        public string[] Backgrounds { get; set; }
        [JsonProperty(PropertyName = "states")]
        public string[] States { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "updated_at")]
        public string UpdatedAt { get; set; }
    }
}
