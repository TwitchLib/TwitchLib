using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.TwitchAPIClasses
{
    /// <summary>Class represents a response from the Chat Badge Twitch API query.</summary>
    public class BadgeResponse
    {
        /// <summary>List of all badges available in channel.</summary>
        public List<Badge> ChannelBadges { get; protected set; } = new List<Badge>();

        /// <summary>BadgeResponse object constructor.</summary>
        public BadgeResponse(string jsonStr)
        {
            JObject json = JObject.Parse(jsonStr);
            foreach (string badgeName in json.Properties().Select(x => x.Name).ToList())
                if(badgeName != "_links")
                    ChannelBadges.Add(new Badge(badgeName, json.SelectToken(badgeName)));
        }

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
}
