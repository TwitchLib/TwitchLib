using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Chat
{
    public class EmoteSet
    {
        #region EmoticonSets
        [JsonProperty(PropertyName = "emoticon_sets")]
        public Dictionary<string, Emote[]> EmoticonSets { get; protected set; }
        #endregion
        #region Emoticons
        [JsonProperty(PropertyName ="emoticons")]
        public Emote[] Emoticons { get; protected set; }
        #endregion
    }
}
