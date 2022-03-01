using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Users
{
    public class UserFollow
    {
        #region CreatedAt
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; protected set; }
        #endregion
        #region Notifications
        [JsonProperty(PropertyName = "notifications")]
        public bool Notifications { get; protected set; }
        #endregion
        #region Channel
        [JsonProperty(PropertyName = "channel")]
        public Channels.Channel Channel { get; protected set; }
        #endregion
    }
}
