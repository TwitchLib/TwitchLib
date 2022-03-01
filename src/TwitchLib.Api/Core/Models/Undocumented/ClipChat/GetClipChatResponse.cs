using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.ClipChat
{
    public class GetClipChatResponse
    {
        [JsonPropertyName("data")]
        public ReChatMessage[] Messages { get; protected set; }
    }
}
