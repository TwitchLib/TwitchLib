using Newtonsoft.Json;

namespace TwitchLib.Models.API.ThirdParty.ModLookup
{
    public class Stats
    {
        [JsonProperty(PropertyName = "relations")]
        public int Relations { get; protected set; }
        [JsonProperty(PropertyName = "channels_total")]
        public int ChannelsTotal { get; protected set; }
        [JsonProperty(PropertyName = "users")]
        public int Users { get; protected set; }
        [JsonProperty(PropertyName = "channels_no_mods")]
        public int ChannelsNoMods { get; protected set; }
        [JsonProperty(PropertyName = "channels_only_broadcaster")]
        public int ChannelsOnlyBroadcaster { get; protected set; }
    }
}
