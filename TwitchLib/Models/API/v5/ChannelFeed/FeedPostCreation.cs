namespace TwitchLib.Models.API.v5.ChannelFeed
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
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
