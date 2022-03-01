using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Root
{
    public class RootToken
    {
        #region Authorization
        /// <summary>Property representing authorization object.</summary>
        [JsonPropertyName("authorization")]
        public RootAuthorization Auth { get; protected set; }
        #endregion
        #region ClientId
        /// <summary>Property representing the Client ID.</summary>
        [JsonPropertyName("client_id")]
        public string ClientId { get; protected set; }
        #endregion
        #region UserId
        /// <summary>Property representing the userId.</summary>
        [JsonPropertyName("user_id")]
        public string UserId { get; protected set; }
        #endregion
        #region Username
        /// <summary>Property representing the username.</summary>
        [JsonPropertyName("user_name")]
        public string Username { get; protected set; }
        #endregion
        #region Valid
        /// <summary>Property representing if the auth token is valid.</summary>
        [JsonPropertyName("valid")]
        public bool Valid { get; protected set; }
        #endregion
    }
}
