using Newtonsoft.Json;
using System;

namespace TwitchLib.Models.API.v5.Root
{
    public class RootResponse
    {
        #region Token
        /// <summary>Property representing token object.</summary>
        [JsonProperty(PropertyName = "token")]
        public Token Token { get; protected set; }
        #endregion
    }

    public class Token
    {
        #region Authorization
        /// <summary>Property representing authorization object.</summary>
        [JsonProperty(PropertyName = "authorization")]
        public Authorization Auth { get; protected set; }
        #endregion
        #region ClientId
        /// <summary>Property representing the Client ID.</summary>
        [JsonProperty(PropertyName = "client_id")]
        public string ClientId { get; protected set; }
        #endregion
        #region UserId
        /// <summary>Property representing the userId.</summary>
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; protected set; }
        #endregion
        #region Username
        /// <summary>Property representing the username.</summary>
        [JsonProperty(PropertyName = "user_name")]
        public string Username { get; protected set; }
        #endregion
        #region valid
        /// <summary>Property representing if the auth token is valid.</summary>
        [JsonProperty(PropertyName = "valid")]
        public bool Valid { get; protected set; }
        #endregion
    }

    public class Authorization
    {
        #region CreatedAt
        /// <summary>Property representing the date time of channel creation.</summary>
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; protected set; }
        #endregion
        #region scopes
        /// <summary>Property representing the scopes.</summary>
        [JsonProperty(PropertyName = "scopes")]
        public string[] Scopes { get; protected set; }
        #endregion
        #region UpdatedAt
        /// <summary>Property representing the date time of last channel update.</summary>
        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedAt { get; protected set; }
        #endregion
    }
}
