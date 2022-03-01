using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Users.Internal
{
    /// <summary>
    /// 
    /// </summary>
    public class UserExtensionState
    {
        /// <inheritdoc />
        [JsonPropertyName("active")]
        public bool Active { get; protected set; }

        /// <inheritdoc />
        [JsonPropertyName("id")]
        public string Id { get; protected set; }

        /// <inheritdoc />
        [JsonPropertyName("version")]
        public string Version { get; protected set; }

        /// <inheritdoc />
        public UserExtensionState(bool active, string id, string version)
        {
            Active = active;
            Id = id;
            Version = version;
        }
    }
}
