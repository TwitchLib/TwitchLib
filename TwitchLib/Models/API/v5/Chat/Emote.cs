namespace TwitchLib.Models.API.v5.Chat
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
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
