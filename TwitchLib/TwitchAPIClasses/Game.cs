using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPIClasses
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
        public BoxUrls Box { get; protected set; }
        /// <summary>Logo class representing Logo image URLs</summary>
        public LogoUrls Logo { get; protected set; }

        /// <summary>Constructor for Game object.</summary>
        public Game(JToken j)
        {
            Name = j.SelectToken("name")?.ToString();
            Popularity = int.Parse(j.SelectToken("popularity")?.ToString());
            Id = int.Parse(j.SelectToken("_id")?.ToString());
            GiantBombId = int.Parse(j.SelectToken("giantbomb_id")?.ToString());
            Box = new BoxUrls(j.SelectToken("box")?.ToString());
            Logo = new LogoUrls(j.SelectToken("logo")?.ToString());
        }

        /// <summary>Class representing game box image in various sizes.</summary>
        public class BoxUrls
        {
            /// <summary>Large box image.</summary>
            public string Large { get; protected set; }
            /// <summary>Medium box image.</summary>
            public string Medium { get; protected set; }
            /// <summary>Small box image.</summary>
            public string Small { get; protected set; }
            /// <summary>Template box image.</summary>
            public string Template { get; protected set; }

            /// <summary>Constructor for BoxUrls.</summary>
            public BoxUrls(JToken j)
            {
                Large = j.SelectToken("large")?.ToString();
                Medium = j.SelectToken("medium")?.ToString();
                Small = j.SelectToken("small")?.ToString();
                Template = j.SelectToken("template")?.ToString();
            }
        }

        /// <summary>Class representing game logo image in various sizes.</summary>
        public class LogoUrls
        {
            /// <summary>Large game logo.</summary>
            public string Large { get; protected set; }
            /// <summary>Medium game logo.</summary>
            public string Medium { get; protected set; }
            /// <summary>Small game logo.</summary>
            public string Small { get; protected set; }
            /// <summary>Template game logo.</summary>
            public string Template { get; protected set; }

            public LogoUrls(JToken j)
            {
                Large = j.SelectToken("large")?.ToString();
                Medium = j.SelectToken("medium")?.ToString();
                Small = j.SelectToken("small")?.ToString();
                Template = j.SelectToken("template")?.ToString();
            }
        }
    }
}
