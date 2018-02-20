using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v5.ChannelFeed
{
    public class FeedPost
    {
        #region Body
        [JsonProperty(PropertyName = "body")]
        public string Body { get; protected set; }
        #endregion
        #region Comments
        [JsonProperty(PropertyName = "comments")]
        public FeedPostComments Comments { get; protected set; }
        #endregion
        #region CreatedAt
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; internal set; }
        #endregion
        #region Deleted
        [JsonProperty(PropertyName = "deleted")]
        public bool Deleted { get; internal set; }
        #endregion
        #region Embeds
        [JsonProperty(PropertyName = "embeds")]
        public object[] Embeds { get; internal set; } //Unknown format by documentation
        #endregion
        #region Emotes
        [JsonProperty(PropertyName = "emotes")]
        public FeedPostEmote[] Emotes { get; internal set; }
        #endregion
        #region Id
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        #endregion
        #region Permissions
        [JsonProperty(PropertyName = "permissions")]
        public Dictionary<string, bool> Permissions { get; protected set; }
        #endregion
        #region Reactions
        [JsonProperty(PropertyName = "reactions")]
        public Dictionary<string, FeedPostReaction> Reactions { get; protected set; }
        #endregion
        #region User
        [JsonProperty(PropertyName = "user")]
        public Users.User User { get; protected set; }
        #endregion
    }
}
