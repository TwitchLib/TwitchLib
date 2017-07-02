using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v5.Bits
{
    public class Images
    {
        [JsonProperty(PropertyName = "dark")]
        public DarkImage Dark { get; set; }
        [JsonProperty(PropertyName = "light")]
        public LightImage Light { get; set; }
    }
}
