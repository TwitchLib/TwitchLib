using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v3.ChannelFeeds
{
    public class PostResponse
    {
        [JsonProperty(PropertyName = "post")]
        public Post Post { get; protected set; }
        [JsonProperty(PropertyName = "tweet")]
        public string TweetURL { get; protected set; }
    }
}
