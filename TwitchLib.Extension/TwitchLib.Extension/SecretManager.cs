using System;
using System.Collections.Generic;
using System.Text;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Linq;

namespace TwitchLib.Extension
{
    public abstract class SecretManager : ISecretManager
    {
        public SecretManager()
        {
            Secrets = Enumerable.Empty<Models.Secret>();
        }
        public string CurrentSecret { get => Secrets.ToList().OrderByDescending(x => x.Expires).First().Content; }

        public IEnumerable<Models.Secret> Secrets { get; protected set; }

        public ClaimsPrincipal Verify(string jwt, out SecurityToken validTokenOverlay)
        {
            ClaimsPrincipal user = null;
            validTokenOverlay = null;
            foreach (var secret in Secrets.ToList().OrderByDescending(x => x.Expires).Where(x => x.Expires > DateTime.Now))
            {
                user = VerifyWithSecret(jwt, secret.Content, out validTokenOverlay);
                if (user != null)
                {
                    break;
                }
            }
            return user;
        }
        
        private ClaimsPrincipal VerifyWithSecret(string jwt, string secret, out SecurityToken validTokenOverlay)
        {
            var validationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(CurrentSecret)),
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true
            };

            var handler = new JwtSecurityTokenHandler();

            try
            {
                return handler.ValidateToken(jwt, validationParameters, out validTokenOverlay);
            }
            catch
            {
                validTokenOverlay = null;
                return null;
            }
        }

        private int GetEpoch()
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;
            return secondsSinceEpoch;
        }
    }
}
