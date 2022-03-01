using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Users
{
    public class UserNotifications
    {
        #region Email
        [JsonProperty(PropertyName = "email")]
        public bool Email { get; protected set; }
        #endregion
        #region Push
        [JsonProperty(PropertyName = "push")]
        public bool Push { get; protected set; }
        #endregion
    }
}
