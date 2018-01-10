using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Extension
{
    public class ExtensionManager
    {
        private Extensions _extensions;
        public ExtensionManager(Extensions extensions)
        {
            _extensions = extensions;
        }
        
        public ClaimsPrincipal Verify(string jwt, out SecurityToken validTokenOverlay)
        {
            ClaimsPrincipal user = null;
            validTokenOverlay = null;


            foreach (var extension in _extensions.Extension.Values)
            {
                user = extension.Verify(jwt, out validTokenOverlay);
                if (user != null)
                {
                    break;
                }
            }
            return user;
        }

        public Extension GetExtension(string extensionId)
        {
            return _extensions.Extension[extensionId];
        }
        
    }
}
