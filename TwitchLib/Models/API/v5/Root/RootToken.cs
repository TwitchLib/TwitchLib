using Newtonsoft.Json;

namespace TwitchLib.Models.API.v5.Root
{
    public class RootToken
    {
        #region Authorization
        /// <summary>Property representing authorization object.</summary>
        [JsonProperty(PropertyName = "authorization")]
        public RootAuthorization Auth { get; protected set; }
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
        #region Valid
        /// <summary>Property representing if the auth token is valid.</summary>
        [JsonProperty(PropertyName = "valid")]
        public bool Valid { get; protected set; }
        #endregion
    }
}
