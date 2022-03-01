using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.ClipChat
{
    public class ReChatMessage
    {
        [JsonPropertyName("type")]
        public string Type { get; protected set; }
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("attributes")]
        public ReChatMessageAttributes Attributes { get; protected set; }
    }
}
