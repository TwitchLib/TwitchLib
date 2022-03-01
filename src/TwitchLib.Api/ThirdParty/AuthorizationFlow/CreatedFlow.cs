using System.Text.Json.Serialization;

namespace TwitchLib.Api.ThirdParty.AuthorizationFlow
{
    public class CreatedFlow
    {
        [JsonPropertyName("message")]
        public string Url { get; protected set; }
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
    }
}
