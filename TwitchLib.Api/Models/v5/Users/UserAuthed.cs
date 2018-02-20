using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v5.Users
{
    public class UserAuthed
    {
        #region Id
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; protected set; }
        #endregion
        #region Bio
        [JsonProperty(PropertyName = "bio")]
        public string Bio { get; protected set; }
        #endregion
        #region CreatedAt
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; protected set; }
        #endregion
        #region DisplayName
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; protected set; }
        #endregion
        #region Email
        [JsonProperty(PropertyName = "email")]
        public string Email { get; protected set; }
        #endregion
        #region EmailVerified
        [JsonProperty(PropertyName = "email_verified")]
        public bool EmailVerified { get; protected set; }
        #endregion
        #region Logo
        [JsonProperty(PropertyName = "logo")]
        public string Logo { get; protected set; }
        #endregion
        #region Name
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }
        #endregion
        #region Notifications
        [JsonProperty(PropertyName = "notifications")]
        public UserNotifications Notifications { get; protected set; }
        #endregion
        #region Partnered
        [JsonProperty(PropertyName = "partnered")]
        public bool Partnered { get; protected set; }
        #endregion
        #region TwitterConnected
        [JsonProperty(PropertyName = "twitter_connected")]
        public bool TwitterConnected { get; protected set; }
        #endregion
        #region Type
        [JsonProperty(PropertyName = "type")]
        public string Type { get; protected set; }
        #endregion
        #region UpdatedAt
        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedAt { get; protected set; }
        #endregion
    }
}
