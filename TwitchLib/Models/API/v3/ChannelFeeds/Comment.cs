using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.ChannelFeeds
{
    public class Comment
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; protected set; }
        [JsonProperty(PropertyName = "deleted")]
        public bool Deleted { get; protected set; }
        [JsonProperty(PropertyName = "body")]
        public string Body { get; protected set; }
        [JsonProperty(PropertyName = "permissions")]
        public List<KeyValuePair<string, bool>> Permissions { get; protected set; } = new List<KeyValuePair<string, bool>>();
        [JsonProperty(PropertyName = "emotes")]
        public List<Emote> Emotes { get; protected set; } = new List<Emote>();
        [JsonProperty(PropertyName = "reactions")]
        public List<Reaction> Reactions { get; protected set; }
        [JsonProperty(PropertyName = "user")]
        public Users.User User { get; protected set; }
    }
}
