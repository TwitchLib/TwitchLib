using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.API.Badge
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
    }
}
