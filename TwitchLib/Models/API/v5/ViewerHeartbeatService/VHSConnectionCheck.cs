using Newtonsoft.Json;

namespace TwitchLib.Models.API.v5.ViewerHeartbeatService
{
    public class VHSConnectionCheck
    {
        #region Identifier
        [JsonProperty(PropertyName = "identifier")]
        public string Identifier { get; protected set; }
        #endregion
    }
}
