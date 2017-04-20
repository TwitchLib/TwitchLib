namespace TwitchLib.Models.API.v5.Communities
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class Moderators
    {
        #region Moderators
        [JsonProperty(PropertyName = "moderators")]
        public Moderator[] Users { get; protected set; }
        #endregion
    }
}
