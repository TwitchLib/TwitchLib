using Newtonsoft.Json;

namespace TwitchLib.Models.API.Undocumented.ClipChat
{
    public class GetClipChatResponse
    {
        [JsonProperty(PropertyName = "data")]
        public ReChatMessage[] Messages { get; protected set; }
    }
}
