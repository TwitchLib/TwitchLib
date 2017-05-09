using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Follows
{
    public class Follows
    {
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; protected set; }
        [JsonProperty(PropertyName = "notifications")]
        public bool Notifications { get; protected set; }
        [JsonProperty(PropertyName = "channel")]
        public Channels.Channel Channel { get; protected set; }
    }
}
