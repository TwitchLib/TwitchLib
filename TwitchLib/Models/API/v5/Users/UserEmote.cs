using Newtonsoft.Json;

namespace TwitchLib.Models.API.v5.Users
{
    public class UserEmote
    {
        #region Code
        [JsonProperty(PropertyName = "code")]
        public string Code { get; protected set; }
        #endregion
        #region Id
        [JsonProperty(PropertyName = "id")]
        public int Id { get; protected set; }
        #endregion
    }
}
