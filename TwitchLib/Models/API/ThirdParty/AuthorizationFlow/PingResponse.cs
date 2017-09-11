using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.ThirdParty.AuthorizationFlow
{
    public class PingResponse
    {
        public bool Success { get; protected set; }
        public string Id { get; protected set; }

        public int Error { get; protected set; }
        public string Message { get; protected set; }

        public List<Enums.AuthScopes> Scopes { get; protected set; }
        public string Token { get; protected set; }
        public string Username { get; protected set; }

        public PingResponse(string jsonStr)
        {
            JObject json = JObject.Parse(jsonStr);
            Success = bool.Parse(json.SelectToken("success").ToString());
            if(!Success)
            {
                Error = int.Parse(json.SelectToken("error").ToString());
                Message = json.SelectToken("message").ToString();
            } else
            {
                Scopes = new List<Enums.AuthScopes>();
                foreach (var scope in json.SelectToken("scopes"))
                    Scopes.Add(Common.Helpers.StringToScope(scope.ToString()));
                Token = json.SelectToken("token").ToString();
                Username = json.SelectToken("username").ToString();
            }
        }
    }
}
