using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Root
{
    public class Token
    {
        public Authorization Authorization { get; protected set; }
        public string Username { get; protected set; }
        public bool Valid { get; protected set; }

        public Token(JToken json)
        {

        }
    }
}
