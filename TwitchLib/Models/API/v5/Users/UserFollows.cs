namespace TwitchLib.Models.API.v5.Users
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
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
