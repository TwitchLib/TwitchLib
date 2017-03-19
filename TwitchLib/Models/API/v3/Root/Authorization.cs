using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Root
{
    public class Authorization
    {
        public List<string> Scopes { get; protected set; } = null;
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        
        public Authorization(JToken json)
        {

        }
    }
}
