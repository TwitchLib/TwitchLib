using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Models.API.ThirdParty.AuthorizationFlow
{
    public class RefreshTokenResponse
    {
        [JsonProperty(PropertyName = "token")]
        public string Token { get; protected set; }
        [JsonProperty(PropertyName = "refresh")]
        public string Refresh { get; protected set; }
    }
}
