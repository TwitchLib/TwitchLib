using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Chat
{
    public class Badge
    {
        public string Alpha { get; protected set; }
        public string Image { get; protected set; }
        public string SVG { get; protected set; }

        public Badge(JToken json)
        {

        }
    }
}
