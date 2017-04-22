using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Follows
{
    public class FollowsResponse
    {
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
        [JsonProperty(PropertyName = "follows")]
        public Follows[] Follows { get; protected set; }
    }
}
