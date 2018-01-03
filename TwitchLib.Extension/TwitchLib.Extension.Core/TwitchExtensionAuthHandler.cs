using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TwitchLib.Extension.Core.Events;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System;

namespace TwitchLib.Extension.Core
{
    public class TwitchExtensionAuthHandler : AuthenticationHandler<TwitchExtensionAuthOptions>
    {
        private readonly Func<string, ISecretManager> _secretManagerFactory;

        public TwitchExtensionAuthHandler(IOptionsMonitor<TwitchExtensionAuthOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            Func<string, ISecretManager> secretManagerFactory) : base(options, logger, encoder, clock)
        {
            _secretManagerFactory = secretManagerFactory;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string authorization = Request.Headers["x-extension-jwt"];

            if (!string.IsNullOrEmpty(authorization))
            {       
                try
                {
                    var user = _secretManagerFactory(Scheme.Name).Verify(authorization, out var validTokenOverlay);

                    if(user == null )
                        return AuthenticateResult.NoResult();
                    
                    var tokenValidatedContext = new TokenValidatedContext(Context, Scheme, Options)
                    {
                        Principal = user,
                        SecurityToken = validTokenOverlay
                    };

                    tokenValidatedContext.Success();
                    return tokenValidatedContext.Result;
                }
                catch
                {
                    return AuthenticateResult.NoResult();
                }
            }

            return AuthenticateResult.NoResult();
        }
    }
}
