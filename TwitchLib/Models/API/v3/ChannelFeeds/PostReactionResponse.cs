using Newtonsoft.Json;
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
        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; protected set; }
        [JsonProperty(PropertyName = "emote_id")]
        public string EmoteId { get; protected set; }
        public Users.User User { get; protected set; }
    }
}
