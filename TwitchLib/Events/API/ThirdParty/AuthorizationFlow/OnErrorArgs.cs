using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Events.API.ThirdParty.AuthorizationFlow
{
    public class OnErrorArgs
    {
        public int Error { get; set; }
        public string Message { get; set; }
    }
}
