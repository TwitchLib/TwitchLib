namespace TwitchLib.Models.API.v5.Communities
{
    #region using directives
    using System;
    using Newtonsoft.Json;
    #endregion
    public class Moderator
    {
        #region DisplayName
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; protected set; }
        #endregion
        #region Id
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; protected set; }
        #endregion
        #region Name
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }
        #endregion
        #region Type
        [JsonProperty(PropertyName = "type")]
        public string Type { get; protected set; }
        #endregion
        #region Bio
        [JsonProperty(PropertyName = "bio")]
        public string Bio { get; protected set; }
        #endregion
        #region CreatedAt
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; protected set; }
        #endregion
        #region UpdatedAt
        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedAt { get; protected set; }
        #endregion
        #region Logo
        [JsonProperty(PropertyName = "logo")]
        public string Logo { get; protected set; }
        #endregion
    }
}
