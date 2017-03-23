using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Users
{
    public class Emote
    {
        public int Id { get; protected set; }
        public string Code { get; protected set; }

        public Emote(JToken json)
        {
            
        }
    }
}
