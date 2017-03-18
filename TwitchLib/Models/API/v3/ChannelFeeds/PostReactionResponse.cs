using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.ChannelFeeds
{
    public class PostReactionResponse
    {
        public string Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public string EmoteId { get; protected set; }
        public Users.User User { get; protected set; }

        public PostReactionResponse(JToken json)
        {

        }
    }
}
