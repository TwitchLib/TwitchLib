using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Root
{
    public class RootResponse
    {
        [JsonProperty(PropertyName = "identified")]
        public bool Identified { get; protected set; }
        [JsonProperty(PropertyName = "token")]
        public Token Token { get; protected set; } 
    }
}
