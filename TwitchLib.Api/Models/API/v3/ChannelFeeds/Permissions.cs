using Newtonsoft.Json;

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
