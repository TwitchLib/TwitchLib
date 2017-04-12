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
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "body")]
        public string Body { get; protected set; }
        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; protected set; }
        [JsonProperty(PropertyName = "comments")]
        public PostComments Comments { get; protected set; }
        [JsonProperty(PropertyName = "deleted")]
        public bool Deleted { get; protected set; }
        [JsonProperty(PropertyName = "emotes")]
        public Emote[] Emotes { get; protected set; }
        [JsonProperty(PropertyName = "permissions")]
        public Permissions Permissions { get; protected set; }
        [JsonProperty(PropertyName = "reactions")]
        public Dictionary<string, Reaction> Reactions { get; protected set; }
        [JsonProperty(PropertyName = "user")]
        public Users.User User { get; protected set; }
    }
}
