namespace TwitchLib.Models.API.Undocumented.ClipChat
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class GetClipChatResponse
    {
        [JsonProperty(PropertyName = "data")]
        public ReChatMessage[] Messages { get; protected set; }
    }
}
