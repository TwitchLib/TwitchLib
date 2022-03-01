using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.Comments
{
    public class Comment
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }
        [JsonPropertyName("created_at")]
        public object CreatedAt { get; set; }
        [JsonPropertyName("updated_at")]
        public object UpdatedAt { get; set; }
        [JsonPropertyName("channel_id")]
        public string ChannelId { get; set; }
        [JsonPropertyName("content_type")]
        public string ContentType { get; set; }
        [JsonPropertyName("content_id")]
        public string ContentId { get; set; }
        [JsonPropertyName("content_offset_seconds")]
        public float ContentOffsetSeconds { get; set; }
        [JsonPropertyName("commenter")]
        public Commenter Commenter { get; set; }
        [JsonPropertyName("source")]
        public string Source { get; set; }
        [JsonPropertyName("state")]
        public string State { get; set; }
        [JsonPropertyName("message")]
        public Message Message { get; set; }
        [JsonPropertyName("more_replies")]
        public bool MoreReplies { get; set; }
    }
}