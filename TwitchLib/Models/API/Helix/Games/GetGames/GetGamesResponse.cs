using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Models.API.Helix.Games.GetGames
{
    public class GetGamesResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Game[] Games { get; protected set; }
    }
}
