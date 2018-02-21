using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v5.Users
{
    public class UserEmotes
    {
        #region EmoteSets
        [JsonProperty(PropertyName = "emoticon_sets")]
        public Dictionary<string, UserEmote[]> EmoteSets { get; protected set; }
        #endregion
    }
}
