using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v5.Communities
{
    public class BannedUsers
    {
        #region Cursor
        [JsonProperty(PropertyName = "_cursor")]
        public string Cursor { get; protected set; }
        #endregion
        #region Users
        [JsonProperty(PropertyName = "banned_users")]
        public BannedUser[] Users { get; protected set; }
        #endregion
    }
}
