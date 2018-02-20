using Newtonsoft.Json;

namespace TwitchLib.Models.API.v5.Chat
{
    public class AllChatEmotes
    {
        #region Emoticons
        [JsonProperty(PropertyName = "emoticons")]
        public AllChatEmote[] Emoticons { get; protected set; }
        #endregion
    }
}
