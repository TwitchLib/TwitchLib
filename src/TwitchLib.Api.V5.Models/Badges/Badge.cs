using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Badges
{
    public class Badge
    {
        #region Versions
        [JsonProperty(PropertyName = "versions")]
        public Dictionary<string, BadgeContent> Versions { get; protected set; }
        #endregion
    }
}
