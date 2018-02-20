using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v5.Communities
{
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
