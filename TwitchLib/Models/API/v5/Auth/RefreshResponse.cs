namespace TwitchLib.Models.API.v5.Auth
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class RefreshResponse
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; protected set; }

        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; protected set; }

        [JsonProperty(PropertyName = "scope")]
        public string[] Scopes { get; protected set; }
    }
}