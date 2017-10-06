using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Helix.StreamsMetadata
{
    public class PlayerOverwatch
    {
        [JsonProperty(PropertyName = "hero")]
        public HeroOverwatch Hero { get; protected set; }
    }
}
