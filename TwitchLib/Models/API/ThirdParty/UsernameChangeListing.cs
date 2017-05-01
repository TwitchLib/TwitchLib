namespace TwitchLib.Models.API.ThirdParty
{
    #region using directives
    using System;
    using Newtonsoft.Json;
    #endregion
    public class UsernameChangeListing
    {
        [JsonProperty(PropertyName = "userid")]
        public string UserId { get; protected set; }
        [JsonProperty(PropertyName = "username_old")]
        public string UsernameOld { get; protected set; }
        [JsonProperty(PropertyName = "username_new")]
        public string UsernameNew { get; protected set; }
        [JsonProperty(PropertyName = "found_at")]
        public DateTime FoundAt { get; protected set; }
    }
}
