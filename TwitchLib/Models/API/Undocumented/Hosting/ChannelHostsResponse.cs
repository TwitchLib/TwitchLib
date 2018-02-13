using Newtonsoft.Json;

namespace TwitchLib.Models.API.Undocumented.Hosting
{
    public class ChannelHostsResponse
    {
        [JsonProperty(PropertyName = "hosts")]
        public HostListing[] Hosts { get; protected set; }
    }
}
