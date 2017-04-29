using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Undocumented.Hosting
{
    public class ChannelHostsResponse
    {
        [JsonProperty(PropertyName = "hosts")]
        public HostListing[] Hosts { get; protected set; }
    }
}
