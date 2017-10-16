using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v5.Authorization
{
    #region using directives
    using System.Collections.Generic;
    using Newtonsoft.Json;
    #endregion
    public class Token
    {

        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; protected set; }
        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; protected set; }
        [JsonProperty(PropertyName = "expires_in")]
        public int Versions { get; protected set; }
        [JsonProperty(PropertyName = "scope")]
        public string Scopes { get; protected set; }

    }
}

