using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.API.Game
{
    /// <summary>Class representing game box image in various sizes.</summary>
    public class Box
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
        public Box(JToken j)
        {
            Large = j.SelectToken("large")?.ToString();
            Medium = j.SelectToken("medium")?.ToString();
            Small = j.SelectToken("small")?.ToString();
            Template = j.SelectToken("template")?.ToString();
        }
    }
}
