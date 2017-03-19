using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Games
{
    public class TopGame
    {
        public Game Game { get; protected set; }
        public int Viewers { get; protected set; }
        public int Channels { get; protected set; }

        public TopGame(JToken json)
        {

        }
    }
}
