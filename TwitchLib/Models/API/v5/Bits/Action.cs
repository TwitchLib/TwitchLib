using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v5.Bits
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
