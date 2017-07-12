using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v5.Bits
{
    public class Tier
    {
        [JsonProperty(PropertyName = "min_bits")]
        public int MinBits { get; set; }
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }
        [JsonProperty(PropertyName = "images")]
        public Images Images { get; set; }
    }
}
