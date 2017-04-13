using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.API.Badge
{
    /// <summary>Class representing a Badge as returned by the Twitch API.</summary>
    public class Badge
    {
        /// <summary>The name of the badge name.</summary>
        public string BadgeName { get; protected set; }
        /// <summary>The alpha version of the badge image.</summary>
        public string Alpha { get; protected set; }
        /// <summary>The default image of the badge.</summary>
        public string Image { get; protected set; }
        /// <summary>The svg version of the badge image.</summary>
        public string SVG { get; protected set; }

        /// <summary>Badge object constructor.</summary>
        public Badge(string badgeName, JToken images)
        {
            BadgeName = badgeName;
            if (images == null)
                return;
            Alpha = images.SelectToken("alpha")?.ToString();
            Image = images.SelectToken("image")?.ToString();
            SVG = images.SelectToken("svg")?.ToString();
        }
    }
}
