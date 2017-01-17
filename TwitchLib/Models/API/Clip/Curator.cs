using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Clip
{
    /// <summary>Model representing the curator of a Twitch Clip</summary>
    public class Curator
    {
        /// <summary>Curator's Twitch assigned unique Id</summary>
        public long Id { get; protected set; }
        /// <summary>Curator name.</summary>
        public string Name { get; protected set; }
        /// <summary>Customizable iteration of the curator's name.</summary>
        public string DisplayName { get; protected set; }
        /// <summary>URL to the curator's channel.</summary>
        public string ChannelUrl { get; protected set; }
        /// <summary>URL of the curator's logo.</summary>
        public string Logo { get; protected set; }

        /// <summary>Curator model constructor.</summary>
        /// <param name="json"></param>
        public Curator(JToken json)
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
