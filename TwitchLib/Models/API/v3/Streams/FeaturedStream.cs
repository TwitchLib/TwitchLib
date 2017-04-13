using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Streams
{
    public class FeaturedStream
    {
        [JsonProperty(PropertyName = "image")]
        public string ImageURL { get; protected set; }
        [JsonProperty(PropertyName = "text")]
        public string Text { get; protected set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }
        [JsonProperty(PropertyName = "sponsored")]
        public bool Sponsored { get; protected set; }
        [JsonProperty(PropertyName = "scheduled")]
        public bool Scheduled { get; protected set; }
        [JsonProperty(PropertyName = "stream")]
        public Stream Stream { get; protected set; }
    }
}
