using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.API
{
    public class PostToChannelFeedResponse
    {
        public string TweetUrl { get; protected set; }
        public Post Post { get; protected set; }

        public PostToChannelFeedResponse(JToken jsonData)
        {
            if (jsonData.SelectToken("tweet") != null)
                TweetUrl = jsonData.SelectToken("tweet").ToString();
            Post = new Post(jsonData.SelectToken("post"));
        }
    }
}
