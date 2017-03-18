using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.ChannelFeeds
{
    public class ChannelFeedResponse
    {
        public int Total { get; protected set; }
        public string Cursor { get; protected set; }
        public List<Post> Posts { get; protected set; } = new List<Post>();

        public ChannelFeedResponse(JToken json)
        {

        }
    }
}
