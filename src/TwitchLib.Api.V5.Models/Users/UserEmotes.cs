using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Users
{
    public class UserEmotes
    {
        #region EmoteSets
        [JsonProperty(PropertyName = "emoticon_sets")]
        public Dictionary<string, UserEmote[]> EmoteSets { get; protected set; }
        #endregion
    }
}
