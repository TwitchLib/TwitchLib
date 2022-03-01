using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Chat
{
    public class AllChatEmotes
    {
        #region Emoticons
        [JsonProperty(PropertyName = "emoticons")]
        public AllChatEmote[] Emoticons { get; protected set; }
        #endregion
    }
}
