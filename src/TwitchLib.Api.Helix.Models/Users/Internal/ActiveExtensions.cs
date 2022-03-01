using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace TwitchLib.Api.Helix.Models.Users.Internal
{
    public class ActiveExtensions
    {
        [JsonPropertyName("panel")]
        public Dictionary<string, UserActiveExtension> Panel { get; protected set; }
        [JsonPropertyName("overlay")]
        public Dictionary<string, UserActiveExtension> Overlay { get; protected set; }
        [JsonPropertyName("component")]
        public Dictionary<string, UserActiveExtension> Component { get; protected set; }
    }
}
