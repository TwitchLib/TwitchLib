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
        public string Id;
        [JsonProperty(PropertyName = "body")]
        public string Body;
        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt;
        [JsonProperty(PropertyName = "comments")]
        public PostComments Comments;
        [JsonProperty(PropertyName = "deleted")]
        public bool Deleted;
        [JsonProperty(PropertyName = "emotes")]
        public Emote[] Emotes;
        [JsonProperty(PropertyName = "permissions")]
        public Permissions Permissions;
        // TODO: Fix this, requires some deserialization magic
        //[JsonProperty(PropertyName = "reactions")]
        //public KeyValuePair<string, Reaction>[] Reactions;
        [JsonProperty(PropertyName = "user")]
        public Users.User User;
    }
}
