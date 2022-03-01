using System.Text.Json.Serialization;

namespace TwitchLib.Api.Auth
{
    public class AuthCodeResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; protected set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; protected set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; protected set; }

        [JsonPropertyName("scope")]
        public string[] Scopes { get; protected set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }
    }
}