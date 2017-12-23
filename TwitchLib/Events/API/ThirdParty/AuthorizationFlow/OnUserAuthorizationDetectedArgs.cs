using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
