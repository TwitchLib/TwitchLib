namespace TwitchLib.Models.API.v5.ChannelFeed
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class MultipleFeedPosts
    {
        #region Cursor
        [JsonProperty(PropertyName = "_cursor")]
        public string Cursor { get; protected set; }
        #endregion
        #region Topic
        [JsonProperty(PropertyName = "_topic")]
        public string Topic { get; protected set; }
        #endregion
        #region Disabled
        [JsonProperty(PropertyName = "_disabled")]
        public bool Disabled { get; protected set; }
        #endregion

    }
}
