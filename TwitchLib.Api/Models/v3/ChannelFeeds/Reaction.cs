using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v3.ChannelFeeds
{
    public class Reaction
    {
        [JsonProperty(PropertyName = "emote")]
        public string Emote { get; set; }
        [JsonProperty(PropertyName = "count")]
        public string Count { get; set; }
        [JsonProperty(PropertyName = "user_ids")]
        public string[] UserIds { get; set; }
    }
}
