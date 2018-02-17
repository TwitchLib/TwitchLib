using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitchLib.Models.API.v5.Badges
{public class GlobalBadgesResponse
    {
        #region BadgeSets
        [JsonProperty(PropertyName = "badge_sets")]
        public Dictionary<string, Badge> Sets { get; protected set; }
        #endregion
    }
}
