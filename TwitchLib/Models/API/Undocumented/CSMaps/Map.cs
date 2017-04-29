using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Undocumented.CSMaps
{
    public class Map
    {
        [JsonProperty(PropertyName = "map")]
        public string MapCode { get; protected set; }
        [JsonProperty(PropertyName = "map_name")]
        public string MapName { get; protected set; }
        [JsonProperty(PropertyName = "map_image")]
        public string MapImage { get; protected set; }
        [JsonProperty(PropertyName = "viewers")]
        public int Viewers { get; protected set; }
    }
}
