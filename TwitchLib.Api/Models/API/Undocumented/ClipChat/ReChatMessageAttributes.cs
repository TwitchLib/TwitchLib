using Newtonsoft.Json;

namespace TwitchLib.Models.API.Undocumented.ClipChat
{
    public class ReChatMessageAttributes
    {
        [JsonProperty(PropertyName = "command")]
        public string Command { get; protected set; }
        [JsonProperty(PropertyName = "room")]
        public string Room { get; protected set; }
        [JsonProperty(PropertyName = "timestamp")]
        public string Timestamp { get; protected set; }
        [JsonProperty(PropertyName = "video-offset")]
        public long VideoOffset { get; protected set; }
        [JsonProperty(PropertyName = "deleted")]
        public bool Deleted { get; protected set; }
        [JsonProperty(PropertyName = "message")]
        public string Message { get; protected set; }
        [JsonProperty(PropertyName = "from")]
        public string From { get; protected set; }
        [JsonProperty(PropertyName = "tags")]
        public ReChatMessageAttributesTags Tags { get; protected set; }
        [JsonProperty(PropertyName = "color")]
        public string Color { get; protected set; }
    }
}
