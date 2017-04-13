using Newtonsoft.Json.Linq;
using System;

namespace TwitchLib.Models.API.Clip
{
    /// <summary>Model for a Twitch Clip</summary>
    public class Clip
    {
        /// <summary>Unique Twitch-assigned Id</summary>
        public string Id { get; protected set; }
        /// <summary>URL to the clip.</summary>
        public string Url { get; protected set; }
        /// <summary>URL with the intended use of being embeded.</summary>
        public string EmbedUrl { get; protected set; }
        /// <summary>HTML snippet needed to embed the clip.</summary>
        public string EmbedHtml { get; protected set; }
        /// <summary>Broadcaster model representing data about the broadcaster in the clip.</summary>
        public Broadcaster Broadcaster { get; protected set; }
        /// <summary>Curator model representing the data about the curator of the clip.</summary>
        public Curator Curator { get; protected set; }
        /// <summary>VOD model representing the VOD that the clip was sourced from.</summary>
        public VOD VOD { get; protected set; }
        /// <summary>Name of the game that the clip features.</summary>
        public string Game { get; protected set; }
        /// <summary>Title of the clip.</summary>
        public string Title { get; protected set; }
        /// <summary>Number of total views the clip has received.</summary>
        public int Views { get; protected set; }
        /// <summary>Duration of the clip.</summary>
        public double Duration { get; protected set; }
        /// <summary>DateTime object representing the creation date.</summary>
        public DateTime CreatedAt { get; protected set; }

        /// <summary>Clip model constructor.</summary>
        /// <param name="json"></param>
        public Clip(JToken json)
        {
            Id = json.SelectToken("id")?.ToString();
            Url = json.SelectToken("url")?.ToString();
            EmbedUrl = json.SelectToken("embed_url")?.ToString();
            EmbedHtml = json.SelectToken("embed_html")?.ToString();
            if (json.SelectToken("broadcaster") != null)
                Broadcaster = new Broadcaster(json.SelectToken("broadcaster"));
            if (json.SelectToken("curator") != null)
                Curator = new Curator(json.SelectToken("curator"));
            if (json.SelectToken("vod") != null)
                VOD = new VOD(json.SelectToken("vod"));
            Game = json.SelectToken("game")?.ToString();
            Title = json.SelectToken("title")?.ToString();
            int viewsParse = -1;
            double durationParse = -1;
            int.TryParse(json.SelectToken("views").ToString(), out viewsParse);
            if (viewsParse != -1)
                Views = viewsParse;
            double.TryParse(json.SelectToken("duration").ToString(), out durationParse);
            if (durationParse != -1)
                Duration = durationParse;
            if (json.SelectToken("created_at") != null)
                CreatedAt = Common.Helpers.DateTimeStringToObject(json.SelectToken("created_at").ToString());
        }
    }
}
