using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace TwitchLib.Extension.Core.Events
{
    public class TokenValidatedContext : ResultContext<TwitchExtensionAuthOptions>
    {
        public TokenValidatedContext(
            HttpContext context,
            AuthenticationScheme scheme,
            TwitchExtensionAuthOptions options)
            : base(context, scheme, options) {}

        public SecurityToken SecurityToken { get; set; }
    }
}
