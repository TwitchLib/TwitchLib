using Newtonsoft.Json;

namespace TwitchLib.Api.Models.Undocumented.ClipChat
{
    public class GetClipChatResponse
    {
        [JsonProperty(PropertyName = "data")]
        public ReChatMessage[] Messages { get; protected set; }
    }
}
