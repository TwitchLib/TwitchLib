using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Chat
{
    public class Emoticon
    {
        [JsonProperty(PropertyName = "regex")]
        public string Regex { get; protected set; }
        [JsonProperty(PropertyName = "images")]
        public Image[] Images { get; protected set; }
    }
}
