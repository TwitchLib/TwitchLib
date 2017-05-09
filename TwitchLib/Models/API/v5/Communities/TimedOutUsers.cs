namespace TwitchLib.Models.API.v5.Communities
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class TimedOutUsers
    {
        #region Cursor
        [JsonProperty(PropertyName = "_cursor")]
        public string Cursor { get; protected set; }
        #endregion
        #region Users
        [JsonProperty(PropertyName = "timed_out_users")]
        public TimedOutUser[] Users { get; protected set; }
        #endregion
    }
}
