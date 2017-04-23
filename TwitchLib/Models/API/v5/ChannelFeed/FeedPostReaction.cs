namespace TwitchLib.Models.API.v5.ChannelFeed
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class FeedPostReaction
    {
        #region Count
        [JsonProperty(PropertyName = "count")]
        public int Count { get; protected set; }
        #endregion
        #region Emote
        [JsonProperty(PropertyName = "emote")]
        public string Emote { get; protected set; }
        #endregion
        #region UserIds
        [JsonProperty(PropertyName = "user_ids")]
        public int[] UserIds { get; protected set; }
        #endregion
    }
}
