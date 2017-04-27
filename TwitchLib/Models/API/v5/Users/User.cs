namespace TwitchLib.Models.API.v5.Users
{
    #region using directives
    using System;
    using Newtonsoft.Json;
    #endregion
    /// <summary>Class representing a User object from Twitch API.</summary>
    public class User
    {
        #region Id
        /// <summary>Property representing the user ID.</summary>
        [JsonProperty(PropertyName = "_id")]
        public long Id { get; internal set; }
        #endregion
        #region Bio
        /// <summary>Property representing the bio.</summary>
        [JsonProperty(PropertyName = "bio")]
        public string Bio { get; internal set; }
        #endregion
        #region CreatedAt
        /// <summary>Property representing the date time of user creation.</summary>
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; internal set; }
        #endregion
        #region DisplayName
        /// <summary>Property representing the case sensitive display name of the user.</summary>
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; internal set; }
        #endregion
        #region Logo
        /// <summary>Property representing the logo of the channel</summary>
        [JsonProperty(PropertyName = "logo")]
        public string Logo { get; internal set; }
        #endregion
        #region Name
        /// <summary>Property representing the name of the user (always in lowercase).</summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; internal set; }
        #endregion
        #region Type
        /// <summary>Property representing the type of the user.</summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; internal set; }
        #endregion
        #region UpdatedAt
        /// <summary>Property representing the date time of last user update.</summary>
        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedAt { get; internal set; }
        #endregion
    }
}
