using System.Text.Json.Serialization;

namespace TwitchLib.Api.ThirdParty.AuthorizationFlow
{
    public class RefreshTokenResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; protected set; }
        [JsonPropertyName("refresh")]
        public string Refresh { get; protected set; }
        [JsonPropertyName("client_id")]
        public string ClientId { get; protected set; }
    }
}
