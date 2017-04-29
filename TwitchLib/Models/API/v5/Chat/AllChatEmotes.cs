namespace TwitchLib.Models.API.v5.Chat
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class AllChatEmotes
    {
        #region Emoticons
        [JsonProperty(PropertyName = "emoticons")]
        public AllChatEmote[] Emoticons { get; protected set; }
        #endregion
    }
}
