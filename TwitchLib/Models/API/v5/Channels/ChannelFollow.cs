namespace TwitchLib.Models.API.v5.Channels
{
    #region using directives
    using System;
    using Newtonsoft.Json;
    #endregion
    /// <summary>Class representing a follow object from Twitch API.</summary>
    public class ChannelFollow
    {
        #region CreatedAt
        /// <summary>Property representing the date time of follow creation.</summary>
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; protected set; }
        #endregion
        #region Notifications
        /// <summary>Property representing wether notifications are activated or not.</summary>
        [JsonProperty(PropertyName = "notifications")]
        public bool Notifications { get; protected set; }
        #endregion
        #region User
        /// <summary>Property representing the User that follows.</summary>
        [JsonProperty(PropertyName = "user")]
        public Users.User User { get; protected set; }
        #endregion
    }
}
