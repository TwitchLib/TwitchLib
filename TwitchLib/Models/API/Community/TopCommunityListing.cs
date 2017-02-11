using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Community
{
    /// <summary>Object representing a single listing from top Twitch communities.</summary>
    public class TopCommunityListing
    {
        /// <summary>Id of Twitch community.</summary>
        public string Id { get; protected set; }
        /// <summary>Name of the Twitch community.</summary>
        public string Name { get; protected set; }
        /// <summary>Number of viewers in the community currently.</summary>
        public int Viewers { get; protected set; }
        /// <summary>Number of live channels currently in the community.</summary>
        public int Channels { get; protected set; }
        /// <summary>Url to the avatar image.</summary>
        public string AvatarImageUrl { get; protected set; }

        /// <summary>Constructor for TopCommunityListing object.</summary>
        public TopCommunityListing(JToken json)
        {
            Id = json.SelectToken("_id")?.ToString();
            Name = json.SelectToken("name")?.ToString();
            AvatarImageUrl = json.SelectToken("avatar_image_url")?.ToString();
            if (json.SelectToken("viewers") != null)
                Viewers = int.Parse(json.SelectToken("viewers").ToString());
            if (json.SelectToken("channels") != null)
                Channels = int.Parse(json.SelectToken("channels").ToString());
        }
    }
}
