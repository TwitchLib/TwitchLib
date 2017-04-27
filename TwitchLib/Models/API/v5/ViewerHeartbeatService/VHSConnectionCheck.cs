namespace TwitchLib.Models.API.v5.ViewerHeartbeatService
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class VHSConnectionCheck
    {
        #region Identifier
        [JsonProperty(PropertyName = "identifier")]
        public string Identifier { get; protected set; }
        #endregion
    }
}
