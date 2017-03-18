using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.ChannelFeeds
{
    public class Post
    {
        public string Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public bool Deleted { get; protected set; }
        public List<Emote> Emotes { get; protected set; } = new List<Emote>();
        public List<Reaction> Reactions { get; protected set; } = new List<Reaction>();
        public string Body { get; protected set; }
        public Users.User User { get; protected set; }

        public Post(JToken json)
        {

        }
    }
}
