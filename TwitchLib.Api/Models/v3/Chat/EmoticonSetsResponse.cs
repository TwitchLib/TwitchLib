using Newtonsoft.Json;
using System.Collections.Generic;

namespace TwitchLib.Api.Models.v3.Chat
{
    public class EmoticonSetsResponse
    {
        [JsonProperty(PropertyName = "emoticon_sets")]
        public Dictionary<string, EmoticonImage[]> EmoticonSets { get; protected set; }
    }
}
