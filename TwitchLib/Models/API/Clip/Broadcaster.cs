using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Clip
{
    /// <summary>Model representing a broadcaster assigned to a Twitch Clip</summary>
    public class Broadcaster
    {
        /// <summary>Broadcaster assigned unique ID</summary>
        public long Id { get; protected set; }
        /// <summary>Broadcaster's name.</summary>
        public string Name { get; protected set; }
        /// <summary>Customizable iteration of broadcaster's name.</summary>
        public string DisplayName { get; protected set; }
        /// <summary>URL to Broadcaster's channel.</summary>
        public string ChannelUrl { get; protected set; }
        /// <summary>URL of Broadcaster's logo.</summary>
        public string Logo { get; protected set; }

        /// <summary>Constructor of Broadcaster model.</summary>
        /// <param name="json"></param>
        public Broadcaster(JToken json)
        {
            long idParse = -1;
            long.TryParse(json.SelectToken("id").ToString(), out idParse);
            if (idParse != -1)
                Id = idParse;
            Name = json.SelectToken("name")?.ToString();
            DisplayName = json.SelectToken("display_name")?.ToString();
            ChannelUrl = json.SelectToken("channel_url")?.ToString();
            Logo = json.SelectToken("logo")?.ToString();
        }
    }
}
