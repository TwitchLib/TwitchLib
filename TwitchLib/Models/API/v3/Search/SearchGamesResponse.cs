using Newtonsoft.Json;
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
        [JsonProperty(PropertyName = "games")]
        public Games.Game[] Games { get; protected set; }
    }
}
