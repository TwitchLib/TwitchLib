using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Games
{
    public class Logo
    {
        public string Large { get; protected set; }
        public string Medium { get; protected set; }
        public string Small { get; protected set; }
        public string Template { get; protected set; }

        public Logo(JToken json)
        {

        }
    }
}
