using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Users.Internal
{
    public class UserExtension
    {
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("version")]
        public string Version { get; protected set; }
        [JsonPropertyName("name")]
        public string Name { get; protected set; }
        [JsonPropertyName("can_activate")]
        public bool CanActivate { get; protected set; }
        [JsonPropertyName("type")]
        public string[] Type { get; protected set; }
    }
}
