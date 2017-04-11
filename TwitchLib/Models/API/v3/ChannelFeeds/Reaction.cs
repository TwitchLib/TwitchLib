using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.ChannelFeeds
{
    public class Reaction
    {
        [JsonProperty(PropertyName = "emote")]
        public string Emote { get; set; }
        [JsonProperty(PropertyName = "count")]
        public string Count { get; set; }
        [JsonProperty(PropertyName = "user_ids")]
        public string[] UserIds { get; set; }
    }
}
