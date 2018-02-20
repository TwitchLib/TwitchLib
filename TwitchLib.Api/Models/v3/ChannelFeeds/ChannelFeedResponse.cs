using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v3.ChannelFeeds
{
    public class ChannelFeedResponse
    {
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
        [JsonProperty(PropertyName = "_cursor")]
        public string Cursor { get; protected set; }
        [JsonProperty(PropertyName = "posts")]
        public Post[] Posts { get; protected set; }
    }
}
