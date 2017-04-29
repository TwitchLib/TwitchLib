namespace TwitchLib.Models.API.v5.Users
{
    #region using directives
    using System;
    using Newtonsoft.Json;
    #endregion
    public class UserBlock
    {
        #region Id
        [JsonProperty(PropertyName = "_id")]
        public long Id { get; protected set; }
        #endregion
        #region UpdatedAt
        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedAt { get; protected set; }
        #endregion
        #region User
        [JsonProperty(PropertyName = "user")]
        public User User { get; protected set; }
        #endregion
    }
}
