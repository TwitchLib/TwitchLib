namespace TwitchLib.Models.API.v5.Users
{
    #region using directives
    using System;
    using Newtonsoft.Json;
    #endregion
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
