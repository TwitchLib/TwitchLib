using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.ChannelFeeds
{
    public class Reaction
    {
        public string Emote { get; protected set; }
        public int Count { get; protected set; }

        public Reaction(JToken json)
        {

        }
    }
}
