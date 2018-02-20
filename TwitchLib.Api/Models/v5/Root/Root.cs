using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v5.Root
{
    public class Root
    {
        #region Token
        /// <summary>Property representing token object.</summary>
        [JsonProperty(PropertyName = "token")]
        public RootToken Token { get; protected set; }
        #endregion
    }
}
