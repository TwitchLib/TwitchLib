namespace TwitchLib.Models.API.v5.Root
{
    #region using directives
    using System;
    using Newtonsoft.Json;
    #endregion
    public class Root
    {
        #region Token
        /// <summary>Property representing token object.</summary>
        [JsonProperty(PropertyName = "token")]
        public RootToken Token { get; protected set; }
        #endregion
    }
}
