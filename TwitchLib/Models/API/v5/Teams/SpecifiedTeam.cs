namespace TwitchLib.Models.API.v5.Teams
{
    #region using directives
    using System;
    using Newtonsoft.Json;
    #endregion
    /// <summary>Class representing a more detailed team object from Twitch API</summary>
    public class SpecifiedTeam
    {
        #region Id
        /// <summary>Property representing the team ID.</summary>
        [JsonProperty(PropertyName = "_id")]
        public long Id { get; protected set; }
        #endregion
        #region Background
        /// <summary>Property representing the background.</summary>
        [JsonProperty(PropertyName = "background")]
        public string Background { get; protected set; }
        #endregion
        #region Banner
        /// <summary>Property representing the banner.</summary>
        [JsonProperty(PropertyName = "banner")]
        public string Banner { get; protected set; }
        #endregion
        #region CreatedAt
        /// <summary>Property representing the date time of team creation.</summary>
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; protected set; }
        #endregion
        #region DisplayName
        /// <summary>Property representing the case sensitive display name of the team.</summary>
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; protected set; }
        #endregion
        #region Info
        /// <summary>Property representing the information of the team.</summary>
        [JsonProperty(PropertyName = "info")]
        public string Info { get; protected set; }
        #endregion
        #region Logo
        /// <summary>Property representing the logo of the team.</summary>
        [JsonProperty(PropertyName = "logo")]
        public string Logo { get; protected set; }
        #endregion
        #region Name
        /// <summary>Property representing the name of the team (always in lowercase).</summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }
        #endregion
        #region UpdatedAt
        /// <summary>Property representing the date time of last team update.</summary>
        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedAt { get; protected set; }
        #endregion
        #region Users (should be called channels in the API, but whatever)
        /// <summary>Property representing the users in the team.</summary>
        [JsonProperty(PropertyName = "users")]
        public Channels.Channel[] Users { get; protected set; }
        #endregion
    }
}
