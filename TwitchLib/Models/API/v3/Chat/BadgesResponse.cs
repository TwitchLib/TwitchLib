using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Chat
{
    public class BadgesResponse
    {
        [JsonProperty(PropertyName = "global_mod")]
        public Badge GlobalMod { get; protected set; }
        [JsonProperty(PropertyName = "admin")]
        public Badge Admin { get; protected set; }
        [JsonProperty(PropertyName = "broadcaster")]
        public Badge Broadcaster { get; protected set; }
        [JsonProperty(PropertyName = "mod")]
        public Badge Mod { get; protected set; }
        [JsonProperty(PropertyName = "staff")]
        public Badge Staff { get; protected set; }
        [JsonProperty(PropertyName = "turbo")]
        public Badge Turbo { get; protected set; }
        [JsonProperty(PropertyName = "subscriber")]
        public Badge Subscriber { get; protected set; }
    }
}
