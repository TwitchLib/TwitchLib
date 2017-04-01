using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.ChannelFeeds
{
    public class PostResponse
    {
        [JsonProperty(PropertyName = "post")]
        public Post Post { get; protected set; }
        [JsonProperty(PropertyName = "tweet")]
        public string TweetURL { get; protected set; }

        public PostResponse(JToken json)
        {

        }
    }
}
