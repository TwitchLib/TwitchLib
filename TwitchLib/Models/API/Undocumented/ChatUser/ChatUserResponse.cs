using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Undocumented.ChatUser
{
    public class ChatUserResponse
    {
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "login")]
        public string Login { get; protected set; }
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; protected set; }
        [JsonProperty(PropertyName = "color")]
        public string Color { get; protected set; }
        [JsonProperty(PropertyName = "is_verified_bot")]
        public bool IsVerifiedBot { get; protected set; }
        [JsonProperty(PropertyName = "badges")]
        public KeyValuePair<string, string>[] Badges { get; protected set; }
    }
}
