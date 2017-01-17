using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.API.Game
{
    /// <summary>Class representing Game object returned from Twitch API.</summary>
    public class Game
    {
        /// <summary>Name of returned game.</summary>
        public string Name { get; protected set; }
        /// <summary>Popularity of returned game [nullable]</summary>
        public int? Popularity { get; protected set; }
        /// <summary>Twitch ID of returned game [nullable]</summary>
        public int? Id { get; protected set; }
        /// <summary>GiantBomb ID of returned game [nullable]</summary>
        public int? GiantBombId { get; protected set; }
        /// <summary>Box class representing Box image URLs</summary>
        public Box Box { get; protected set; }
        /// <summary>Logo class representing Logo image URLs</summary>
        public Logo Logo { get; protected set; }

        /// <summary>Constructor for Game object.</summary>
        public Game(JToken j)
        {
            Name = j.SelectToken("name")?.ToString();
            Popularity = int.Parse(j.SelectToken("popularity")?.ToString());
            Id = int.Parse(j.SelectToken("_id")?.ToString());
            GiantBombId = int.Parse(j.SelectToken("giantbomb_id")?.ToString());
            Box = new Box(j.SelectToken("box")?.ToString());
            Logo = new Logo(j.SelectToken("logo")?.ToString());
        }
    }
}
