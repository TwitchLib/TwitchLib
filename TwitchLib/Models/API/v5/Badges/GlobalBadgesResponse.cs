namespace TwitchLib.Models.API.v5.Badges
{
    #region using directives
    using System.Collections.Generic;
    using Newtonsoft.Json;
    #endregion
    public class GlobalBadgesResponse
    {
        #region BadgeSets
        [JsonProperty(PropertyName = "badge_sets")]
        public Dictionary<string, Badge> Sets { get; protected set; }
        #endregion
    }
}
