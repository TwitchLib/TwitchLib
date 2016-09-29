using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPIClasses
{
    /// <summary>Class representing a single featured stream from a Twitch API request.</summary>
    public class FeaturedStream
    {
        /// <summary>Property representing a basic text description of the featured channel. (THIS HAS HTML ELEMENTS, BE AWARE)</summary>
        public string Text { get; protected set; }
        /// <summary>Property representing whether or not the featured channel is sponsored.</summary>
        public bool Sponsored { get; protected set; }
        /// <summary>Property representing the priority of the featured stream (scale from 1-10? looks like 5 and 10 are only in use)</summary>
        public int Priority { get; protected set; }
        /// <summary>Property representing whether or not a stream is a scheduled feature.</summary>
        public bool Scheduled { get; protected set; }
        /// <summary>Property representing the image shown in the tite on the home page.</summary>
        public string Image { get; protected set; }
        /// <summary>Property representing the stream object housing all stream details.</summary>
        public Stream Stream { get; protected set; }

        /// <summary>FeaturedStream constructor.</summary>
        public FeaturedStream(JToken featuredStream)
        {
            bool sponsored, scheduled;

            Text = featuredStream.SelectToken("text")?.ToString();
            if (featuredStream.SelectToken("sponsored") != null)
                Sponsored = bool.TryParse(featuredStream.SelectToken("sponsored").ToString(), out sponsored) && sponsored;
            if (featuredStream.SelectToken("priority") != null)
                Priority = int.Parse(featuredStream.SelectToken("priority").ToString());
            Scheduled = bool.TryParse(featuredStream.SelectToken("scheduled").ToString(), out scheduled) && scheduled;
            Image = featuredStream.SelectToken("image")?.ToString();
            if (featuredStream.SelectToken("stream") != null)
                Stream = new Stream(featuredStream.SelectToken("stream"));
        }
    }
}
