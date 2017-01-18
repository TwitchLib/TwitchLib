using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Video
{
    /// <summary>Model representing the various thumbnail URLs.</summary>
    public class Thumbnails
    {
        /// <summary>List of small thumbnails</summary>
        public Thumbnail Small { get; protected set; }
        /// <summary>List of medium thumbnails</summary>
        public Thumbnail Medium { get; protected set; }
        /// <summary>List of large thumbnails</summary>
        public Thumbnail Large { get; protected set; }
        /// <summary>List of template thumbnails</summary>
        public Thumbnail Template { get; protected set; }

        /// <summary>Constructor for Thumbnails</summary>
        /// <param name="json"></param>
        public Thumbnails(JToken json)
        {
            if (json.SelectToken("small") != null)
                Small = new Thumbnail(json.SelectToken("small"));
            if (json.SelectToken("medium") != null)
                Medium = new Thumbnail(json.SelectToken("medium"));
            if (json.SelectToken("large") != null)
                Large = new Thumbnail(json.SelectToken("large"));
            if (json.SelectToken("template") != null)
                Template = new Thumbnail(json.SelectToken("template"));
        }

        /// <summary>Model representing details about a single Thumbnail</summary>
        public class Thumbnail
        {
            /// <summary>The type of the Thumbnail</summary>
            public string Type { get; protected set; }
            /// <summary>The URL to the Thumbnail</summary>
            public string URL { get; protected set; }

            /// <summary>Constructor for Thumbnail</summary>
            /// <param name="json"></param>
            public Thumbnail(JToken json)
            {
                Type = json.SelectToken("type")?.ToString();
                URL = json.SelectToken("url")?.ToString();
            }
        }
    }
}
