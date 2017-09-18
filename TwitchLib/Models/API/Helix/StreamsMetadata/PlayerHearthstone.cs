using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Helix.StreamsMetadata
{
    public class PlayerHearthstone
    {
        [JsonProperty(PropertyName = "hero")]
        public HeroHearthstone Hero { get; protected set; }
    }
}
