using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Search
{
    public class SearchGamesResponse
    {
        public List<Games.Game> Games { get; protected set; } = new List<v3.Games.Game>();

        public SearchGamesResponse(JToken json)
        {

        }
    }
}
