using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.API.Video
{
    /// <summary>Model representing the links to various preview images.</summary>
    public class Preview
    {
        /// <summary>Small preview image URL.</summary>
        public string Small { get; protected set; }
        /// <summary>Medium preview image URL.</summary>
        public string Medium { get; protected set; }
        /// <summary>Large preview image URL.</summary>
        public string Large { get; protected set; }
        /// <summary>Template preview image URL.</summary>
        public string Template { get; protected set; }

        /// <summary>Preview  constructor.</summary>
        /// <param name="json"></param>
        public Preview(JToken json)
        {
            Small = json.SelectToken("small")?.ToString();
            Medium = json.SelectToken("medium")?.ToString();
            Large = json.SelectToken("large")?.ToString();
            Template = json.SelectToken("template")?.ToString();
        }
    }
}
