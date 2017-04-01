using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.ChannelFeeds
{
    public class Permissions
    {
        [JsonProperty(PropertyName = "can_reply")]
        public bool CanReply;
        [JsonProperty(PropertyName = "can_mmoderate")]
        public bool CanModerate;
        [JsonProperty(PropertyName = "can_delete")]
        public bool CanDelete;
    }
}
