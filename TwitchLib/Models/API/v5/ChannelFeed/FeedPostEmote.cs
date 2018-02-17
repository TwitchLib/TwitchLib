using Newtonsoft.Json;

namespace TwitchLib.Models.API.v5.ChannelFeed
{
    public class FeedPostEmote
    {
        #region End
        [JsonProperty(PropertyName = "end")]
        public int End { get; protected set; }
        #endregion
        #region Id
        [JsonProperty(PropertyName = "id")]
        public int Id { get; protected set; }
        #endregion
        #region Set
        [JsonProperty(PropertyName = "set")]
        public int Set { get; protected set; }
        #endregion
        #region Start
        [JsonProperty(PropertyName = "start")]
        public int Start { get; protected set; }
        #endregion
    }
}
