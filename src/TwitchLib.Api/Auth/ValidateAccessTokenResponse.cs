using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Auth
{
    public class ValidateAccessTokenResponse
    {
        [JsonPropertyName("client_id")]
        public string ClientId { get; protected set; }
        [JsonPropertyName("login")]
        public string Login { get; protected set; }
        [JsonPropertyName("scopes")]
        public List<string> Scopes { get; protected set; }
        [JsonPropertyName("user_id")]
        public string UserId { get; protected set; }
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; protected set; }
    }
}
