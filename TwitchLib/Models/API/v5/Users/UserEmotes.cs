namespace TwitchLib.Models.API.v5.Users
{
    #region using directives
    using System.Collections.Generic;
    using Newtonsoft.Json;
    #endregion
    public class UserEmotes
    {
        #region EmoteSets
        [JsonProperty(PropertyName = "emoticon_sets")]
        public Dictionary<string, UserEmote[]> EmoteSets { get; protected set; }
        #endregion
    }
}
