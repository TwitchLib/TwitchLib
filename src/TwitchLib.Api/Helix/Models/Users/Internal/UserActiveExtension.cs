using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Users.Internal
{
    /// <summary>
    /// 
    /// </summary>
    public class UserActiveExtension
    {
        [JsonPropertyName("active")]
        public bool Active { get; protected set; }
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("version")]
        public string Version { get; protected set; }
        [JsonPropertyName("name")]
        public string Name { get; protected set; }
        [JsonPropertyName("x")]
        public int X { get; protected set; }
        [JsonPropertyName("y")]
        public int Y { get; protected set; }
    }
}
