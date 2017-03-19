using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Games
{
    public class Game
    {
        public string Name { get; protected set; }
        public Box Box { get; protected set; }
        public Logo Logo { get; protected set; }
        public string Id { get; protected set; }
        public int GiantBombId { get; protected set; }

        public Game(JToken json)
        {

        }
    }
}
