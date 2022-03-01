using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Users.Internal
{
    public class UserExtensionState
    {
        [JsonPropertyName("active")]
        public bool Active { get; protected set; }
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("version")]
        public string Version { get; protected set; }

        public UserExtensionState(bool active, string id, string version)
        {
            Active = active;
            Id = id;
            Version = version;
        }
    }
}
