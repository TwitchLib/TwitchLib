using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.API.Game
{
    /// <summary>Class representing game logo image in various sizes.</summary>
    public class Logo
    {
        /// <summary>Large game logo.</summary>
        public string Large { get; protected set; }
        /// <summary>Medium game logo.</summary>
        public string Medium { get; protected set; }
        /// <summary>Small game logo.</summary>
        public string Small { get; protected set; }
        /// <summary>Template game logo.</summary>
        public string Template { get; protected set; }

        /// <summary>LogoUrls object constructor.</summary>
        public Logo(JToken j)
        {
            Large = j.SelectToken("large")?.ToString();
            Medium = j.SelectToken("medium")?.ToString();
            Small = j.SelectToken("small")?.ToString();
            Template = j.SelectToken("template")?.ToString();
        }
    }
}
