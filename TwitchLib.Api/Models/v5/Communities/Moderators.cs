using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v5.Communities
{
    public class Moderators
    {
        #region Moderators
        [JsonProperty(PropertyName = "moderators")]
        public Moderator[] Users { get; protected set; }
        #endregion
    }
}
