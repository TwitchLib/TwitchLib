using Newtonsoft.Json;
using System.Collections.Generic;

namespace TwitchLib.Models.API.v3.Chat
{
    public class SetEmoticonsResponse
    {
        [JsonProperty(PropertyName = "emoticon_sets")]
        public Dictionary<string, EmoticonImage[]> EmoticonSets { get; protected set; }
    }
}
