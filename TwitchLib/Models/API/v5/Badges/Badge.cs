namespace TwitchLib.Models.API.v5.Badges
{
    #region using directives
    using System.Collections.Generic;
    using Newtonsoft.Json;
    #endregion
    public class Badge
    {
        #region Versions
        [JsonProperty(PropertyName = "versions")]
        public Dictionary<string, BadgeContent> Versions { get; protected set; }
        #endregion
    }
}
