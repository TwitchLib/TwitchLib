namespace TwitchLib.Models.API.v5.Communities
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
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
