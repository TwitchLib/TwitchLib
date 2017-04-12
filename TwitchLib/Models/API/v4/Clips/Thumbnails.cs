using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v4.Clips
{
    public class Thumbnails
    {
        [JsonProperty(PropertyName = "medium")]
        public string Medium { get; protected set; }
        [JsonProperty(PropertyName = "small")]
        public string Small { get; protected set; }
        [JsonProperty(PropertyName = "tiny")]
        public string Tiny { get; protected set; }
    }
}
