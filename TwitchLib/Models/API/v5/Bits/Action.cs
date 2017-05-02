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
        public string Prefix { get; set; }
        public string[] Scales { get; set; }
        public Tier[] Tiers { get; set; }
        public string[] Backgrounds { get; set; }
        public string[] States { get; set; }
        public string Type { get; set; }
        [JsonProperty(PropertyName = "updated_at")]
        public string UpdatedAt { get; set; }
    }
}
