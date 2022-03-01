using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Chat
{
    public class Emote
    {
        #region Code
        [JsonProperty(PropertyName = "code")]
        public string Code { get; protected set; }
        #endregion
        #region EmoticonSet
        [JsonProperty(PropertyName = "emoticon_set")]
        public int EmoticonSet { get; protected set; }
        #endregion
        #region Id
        [JsonProperty(PropertyName = "id")]
        public int Id { get; protected set; }
        #endregion
    }
}
