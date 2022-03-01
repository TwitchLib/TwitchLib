using System.Collections.Generic;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Events
{
    public class OnUserAuthorizationDetectedArgs
    {
        public string Id { get; set; }
        public List<AuthScopes> Scopes { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string Refresh { get; set; }
        public string ClientId { get; set; }
    }
}
