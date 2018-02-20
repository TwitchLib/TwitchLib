using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v3.ChannelFeeds
{
    public class Emote
    {
        [JsonProperty(PropertyName = "end")]
        public int End { get; protected set; }
        [JsonProperty(PropertyName = "id")]
        public int Id { get; protected set; }
        [JsonProperty(PropertyName = "set")]
        public int Set { get; protected set; }
        [JsonProperty(PropertyName = "start")]
        public int Start { get; protected set; }
    }
}
