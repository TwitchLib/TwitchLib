using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v5.ChannelFeed
{
    public class FeedPostCreation
    {
        #region Post
        [JsonProperty(PropertyName = "post")]
        public FeedPost Post { get; internal set; }
        #endregion
        #region Tweet
        [JsonProperty(PropertyName = "tweet")]
        public string Tweet { get; internal set; }
        #endregion
    }
}
