using Newtonsoft.Json;

namespace TwitchLib.Models.API.Undocumented.Comments
{
    public class Comment
    {
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "created_at")]
        public object CreatedAt { get; set; }
        [JsonProperty(PropertyName = "updated_at")]
        public object UpdatedAt { get; set; }
        [JsonProperty(PropertyName = "channel_id")]
        public string ChannelId { get; set; }
        [JsonProperty(PropertyName = "content_type")]
        public string ContentType { get; set; }
        [JsonProperty(PropertyName = "content_id")]
        public string ContentId { get; set; }
        [JsonProperty(PropertyName = "content_offset_seconds")]
        public float ContentOffsetSeconds { get; set; }
        [JsonProperty(PropertyName = "commenter")]
        public Commenter Commenter { get; set; }
        [JsonProperty(PropertyName = "source")]
        public string Source { get; set; }
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }
        [JsonProperty(PropertyName = "message")]
        public Message Message { get; set; }
        [JsonProperty(PropertyName = "more_replies")]
        public bool MoreReplies { get; set; }
    }
}