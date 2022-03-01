using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.ViewerHeartbeatService
{
    public class VHSConnectionCheck
    {
        #region Identifier
        [JsonProperty(PropertyName = "identifier")]
        public string Identifier { get; protected set; }
        #endregion
    }
}
