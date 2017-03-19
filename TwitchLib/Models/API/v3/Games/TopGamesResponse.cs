using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Games
{
    public class TopGamesResponse
    {
        public int Total { get; protected set; }
        public List<TopGame> TopGames { get; protected set; } = new List<TopGame>();

        public TopGamesResponse(JToken json)
        {

        }
    }
}
