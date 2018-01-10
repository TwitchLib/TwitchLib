using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Extension
{
    public class Extension
    {
        private readonly API _api;
        private readonly ExtensionConfiguration _config;

        public Extension(API api, ExtensionConfiguration config)
        {
            _api = api;
            _config = config;
        }
        
        public string GetCurrentSecret()
        {
            return _config.SecretHandler.CurrentSecret;
        }

        public ClaimsPrincipal Verify(string jwt, out SecurityToken validTokenOverlay)
        {
            var user =  _config.SecretHandler.Verify(jwt, out validTokenOverlay);
            if(user != null)
            {
                ((ClaimsIdentity)user.Identity).AddClaim(new Claim("extension_id", _config.Id, ClaimValueTypes.String));
            }
            return user;
        }
    }
}
