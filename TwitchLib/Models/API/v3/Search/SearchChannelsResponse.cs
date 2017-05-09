using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Search
{
    public class SearchChannelsResponse
    {
        [JsonProperty(PropertyName = "channels")]
        public Channels.Channel[] Channels { get; protected set; }
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
    }
}
