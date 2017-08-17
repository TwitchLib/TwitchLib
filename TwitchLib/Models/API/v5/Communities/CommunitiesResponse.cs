using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v5.Communities
{
    public class CommunitiesResponse
    {
        [JsonProperty(PropertyName = "communities")]
        public Community[] Communities { get; protected set; }
    }
}
