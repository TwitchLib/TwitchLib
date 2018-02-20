using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v5.ChannelFeed
{
    public class FeedPostReactionPost
    {
        #region CreatedAt
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; internal set; }
        #endregion
        #region EmoteId
        [JsonProperty(PropertyName = "emote_id")]
        public string EmoteId { get; internal set; }
        #endregion
        #region Id
        [JsonProperty(PropertyName = "id")]
        public string Id { get; internal set; }
        #endregion
        #region User
        [JsonProperty(PropertyName = "user")]
        public Users.User User { get; internal set; }
        #endregion
    }
}
