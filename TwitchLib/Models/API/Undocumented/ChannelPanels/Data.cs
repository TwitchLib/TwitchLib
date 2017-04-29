using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Undocumented.ChannelPanels
{
    public class Data
    {
        [JsonProperty(PropertyName = "link")]
        public string Link { get; protected set; }
        [JsonProperty(PropertyName = "image")]
        public string Image { get; protected set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }
    }
}
