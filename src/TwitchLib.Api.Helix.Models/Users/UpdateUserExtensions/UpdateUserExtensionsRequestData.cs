using System.Text.Json.Serialization;
using System.Collections.Generic;
using TwitchLib.Api.Helix.Models.Users.Internal;

namespace TwitchLib.Api.Helix.Models.Users.UpdateUserExtensions
{
    public class UpdateUserExtensionsRequestData
    {
        [JsonPropertyName("panel")]
        public Dictionary<string, UserExtensionState> Panel { get; set; }
        [JsonPropertyName("component")]
        public Dictionary<string, UserExtensionState> Component { get; set; }
        [JsonPropertyName("overlay")]
        public Dictionary<string, UserExtensionState> Overlay { get; set; }
    }
}
