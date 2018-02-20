using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v5.Chat
{
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
