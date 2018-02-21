using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v5.Auth
{
    public class RefreshResponse
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; protected set; }

        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; protected set; }
        
        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; protected set; }

        [JsonProperty(PropertyName = "scope")]
        public string[] Scopes { get; protected set; }
    }
}
