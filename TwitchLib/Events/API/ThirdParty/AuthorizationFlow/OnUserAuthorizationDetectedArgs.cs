using System.Collections.Generic;

namespace TwitchLib.Events.API.ThirdParty.AuthorizationFlow
{
    public class OnUserAuthorizationDetectedArgs
    {
        public string Id { get; set; }
        public List<Enums.AuthScopes> Scopes { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string Refresh { get; set; }
    }
}
