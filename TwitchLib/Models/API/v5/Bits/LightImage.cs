using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v5.Bits
{
    public class LightImage
    {
        [JsonProperty(PropertyName = "animated")]
        public ImageLinks Animated { get; set; }
        [JsonProperty(PropertyName = "static")]
        public ImageLinks Static { get; set; }
    }
}
