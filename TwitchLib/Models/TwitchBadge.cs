using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib
{
    class TwitchBadge
    {
        public string Alpha { get; }
        public string Image { get; }
        public string Svg { get; }

        public TwitchBadge(JToken json)
        {
            Alpha = json.SelectToken("alpha")?.ToString();
            Image = json.SelectToken("image")?.ToString();
            Svg = json.SelectToken("svg")?.ToString();
        }
    }
}
