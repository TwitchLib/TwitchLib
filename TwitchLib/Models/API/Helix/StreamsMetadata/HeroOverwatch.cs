using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Helix.StreamsMetadata
{
    public class HeroOverwatch
    {
        [JsonProperty(PropertyName = "ability")]
        public string Ability { get; protected set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }
        [JsonProperty(PropertyName = "role")]
        public string Role { get; protected set; }
    }
}
