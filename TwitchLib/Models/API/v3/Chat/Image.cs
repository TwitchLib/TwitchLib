using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Chat
{
    public class Image
    {
        [JsonProperty(PropertyName = "emoticon_set", NullValueHandling = NullValueHandling.Ignore)]
        public int EmoticonSet { get; protected set; }
        [JsonProperty(PropertyName = "height")]
        public int Height { get; protected set; }
        [JsonProperty(PropertyName = "width")]
        public int Width { get; protected set; }
        [JsonProperty(PropertyName = "url")]
        public string URL { get; protected set; }
    }
}
