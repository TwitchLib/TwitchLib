namespace TwitchLib.Models.API.v5.Chat
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class AllChatEmote
    {
        #region Regex
        [JsonProperty(PropertyName = "regex")]
        public string Regex { get; protected set; }
        #endregion
        #region Images
        [JsonProperty(PropertyName = "images")]
        public EmoticonImage[] Images{ get; protected set; }
        #endregion
    }
}
