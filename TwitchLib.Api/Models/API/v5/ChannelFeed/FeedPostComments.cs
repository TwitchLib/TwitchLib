using Newtonsoft.Json;

namespace TwitchLib.Models.API.v5.ChannelFeed
{
    public class FeedPostComments
    {
        #region Cursor
        [JsonProperty(PropertyName = "_cursor")]
        public string Cursor { get; protected set; }
        #endregion
        #region Total
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
        #endregion
        #region Comments
        [JsonProperty(PropertyName = "comments")]
        public FeedPostComment[] Comments { get; protected set; }
        #endregion
    }
}
