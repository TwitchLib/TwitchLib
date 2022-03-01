using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Users
{
    public class UserFollows
    {
        #region Total
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
        #endregion
        #region Follows
        [JsonProperty(PropertyName = "follows")]
        public UserFollow[] Follows { get; protected set; }
        #endregion
    }
}
