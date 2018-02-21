using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v5.Users
{
    public class Users
    {
        #region Total
        [JsonProperty(PropertyName ="_total")]
        public int Total { get; protected set; }
        #endregion
        #region Users
        [JsonProperty(PropertyName ="users")]
        public User[] Matches { get; protected set; }
        #endregion
    }
}
