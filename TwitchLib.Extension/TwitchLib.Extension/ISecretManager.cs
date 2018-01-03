using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Security.Claims;

namespace TwitchLib.Extension
{
    public interface ISecretManager
    {
        string CurrentSecret { get; }
        IEnumerable<Models.Secret> Secrets { get;  }
        ClaimsPrincipal Verify(string jwt, out SecurityToken validTokenOverlay);
    }
}
