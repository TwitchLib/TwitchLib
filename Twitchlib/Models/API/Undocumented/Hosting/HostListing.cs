using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Undocumented.Hosting
{
    public class HostListing
    {
        [JsonProperty(PropertyName = "host_id")]
        public string HostId { get; protected set; }
        [JsonProperty(PropertyName = "target_id")]
        public string TargetId { get; protected set; }
        [JsonProperty(PropertyName = "host_login")]
        public string HostLogin { get; protected set; }
        [JsonProperty(PropertyName = "target_login")]
        public string TargetLogin { get; protected set; }
        [JsonProperty(PropertyName = "host_display_name")]
        public string HostDisplayName { get; protected set; }
        [JsonProperty(PropertyName = "target_display_name")]
        public string TargetDisplayName { get; protected set; }
    }
}
