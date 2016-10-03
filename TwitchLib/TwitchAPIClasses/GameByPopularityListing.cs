using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPIClasses
{
    /// <summary>Class representing a game by popularity listing received from Twitch Api.</summary>
    public class GameByPopularityListing
    {
        /// <summary>Property representing the Game object.</summary>
        public Game Game { get; protected set; }
        /// <summary>Property representing the number of viewers the game currently has.</summary>
        public int Viewers { get; protected set; }
        /// <summary>Property representing the number of channels currently broadcasting the game.</summary>
        public int Channels { get; protected set; }

        /// <summary>Constructor for GameByPopularityListing</summary>
        public GameByPopularityListing(JToken json)
        {
            if(json.SelectToken("game") != null)
                Game = new Game(json.SelectToken("game"));
            if (json.SelectToken("viewers") != null)
                Viewers = int.Parse(json.SelectToken("viewers").ToString());
            if (json.SelectToken("channels") != null)
                Channels = int.Parse(json.SelectToken("channels").ToString());
        }
    }
}
