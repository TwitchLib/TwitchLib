using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.ClipChat
{
    public class ReChatMessageAttributes
    {
        [JsonPropertyName("command")]
        public string Command { get; protected set; }
        [JsonPropertyName("room")]
        public string Room { get; protected set; }
        [JsonPropertyName("timestamp")]
        public string Timestamp { get; protected set; }
        [JsonPropertyName("video-offset")]
        public long VideoOffset { get; protected set; }
        [JsonPropertyName("deleted")]
        public bool Deleted { get; protected set; }
        [JsonPropertyName("message")]
        public string Message { get; protected set; }
        [JsonPropertyName("from")]
        public string From { get; protected set; }
        [JsonPropertyName("tags")]
        public ReChatMessageAttributesTags Tags { get; protected set; }
        [JsonPropertyName("color")]
        public string Color { get; protected set; }
    }
}
