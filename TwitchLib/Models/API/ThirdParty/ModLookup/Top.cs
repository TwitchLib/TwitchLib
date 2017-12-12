using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.ThirdParty.ModLookup
{
    public class Top
    {
        [JsonProperty(PropertyName = "modcount")]
        public ModLookupListing[] ModCount { get; protected set; }
        [JsonProperty(PropertyName = "views")]
        public ModLookupListing[] Views { get; protected set; }
        [JsonProperty(PropertyName = "followers")]
        public ModLookupListing[] Followers { get; protected set; }
    }
}
