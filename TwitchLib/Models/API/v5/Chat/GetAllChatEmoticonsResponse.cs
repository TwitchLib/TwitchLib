namespace TwitchLib.Models.API.v5.Chat
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class GetAllChatEmoticonsResponse
    {
        #region Emoticons
        [JsonProperty(PropertyName = "emoticons")]
        public AllChatEmote[] Emoticons { get; protected set; }
        #endregion
    }
}
