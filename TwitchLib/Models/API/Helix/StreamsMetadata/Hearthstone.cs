using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Helix.StreamsMetadata
{
    public class Hearthstone
    {
        [JsonProperty(PropertyName = "broadcaster")]
        public PlayerHearthstone Broadcaster { get; protected set; }
        [JsonProperty(PropertyName = "opponent")]
        public PlayerHearthstone Opponent { get; protected set; }
    }
}
