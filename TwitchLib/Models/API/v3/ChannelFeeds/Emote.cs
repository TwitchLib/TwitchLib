using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.ChannelFeeds
{
    public class Emote
    {
        [JsonProperty(PropertyName = "end")]
        public int End { get; protected set; }
        [JsonProperty(PropertyName = "id")]
        public int Id { get; protected set; }
        [JsonProperty(PropertyName = "set")]
        public int Set { get; protected set; }
        [JsonProperty(PropertyName = "start")]
        public int Start { get; protected set; }
    }
}
