using System;
using Newtonsoft.Json;
using TwitchLib.Api.Interfaces;

namespace TwitchLib.Api.Models.v5.Channels
{
    /// <summary>Class representing a follow object from Twitch API.</summary>
    public class ChannelFollow : IFollow
    {
        public ChannelFollow(IUser user)
        {
            User = user;
        }
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
        public IUser User { get; protected set; }
        #endregion
    }
}
