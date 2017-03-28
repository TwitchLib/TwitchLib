using Newtonsoft.Json;
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
        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; protected set; }
        public bool Deleted { get; protected set; }
        public Emote[] Emotes { get; protected set; }
        public Reaction[] Reactions { get; protected set; }
        public string Body { get; protected set; }
        public Users.User User { get; protected set; }
    }
}
