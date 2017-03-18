using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Chat
{
    public class BadgesResponse
    {
        public Badge GlobalMod { get; protected set; }
        public Badge Admin { get; protected set; }
        public Badge Broadcaster { get; protected set; }
        public Badge Mod { get; protected set; }
        public Badge Staff { get; protected set; }
        public Badge Turbo { get; protected set; }
        public Badge Subscriber { get; protected set; }

        public BadgesResponse(JToken json)
        {

        }
    }
}
