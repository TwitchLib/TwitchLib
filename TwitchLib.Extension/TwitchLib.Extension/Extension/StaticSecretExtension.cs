using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Extension.Models;

namespace TwitchLib.Extension
{
    public class StaticSecretExtension : ExtensionBase
    {

        public StaticSecretExtension(ExtensionConfiguration config) : base(config)
        {

        }
    }
}
