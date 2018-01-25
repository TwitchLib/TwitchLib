using Newtonsoft.Json;

namespace TwitchLib.Models.API.Undocumented.Comments
{
    public class Comment
    {
        [JsonProperty(PropertyName = "_id")]
        public string _id { get; set; }
        [JsonProperty(PropertyName = "created_at")]
        public object created_at { get; set; }
        [JsonProperty(PropertyName = "updated_at")]
        public object updated_at { get; set; }
        [JsonProperty(PropertyName = "channel_id")]
        public string channel_id { get; set; }
        [JsonProperty(PropertyName = "content_type")]
        public string content_type { get; set; }
        [JsonProperty(PropertyName = "content_id")]
        public string content_id { get; set; }
        [JsonProperty(PropertyName = "content_offset_seconds")]
        public float content_offset_seconds { get; set; }
        [JsonProperty(PropertyName = "commenter")]
        public Commenter commenter { get; set; }
        [JsonProperty(PropertyName = "source")]
        public string source { get; set; }
        [JsonProperty(PropertyName = "state")]
        public string state { get; set; }
        [JsonProperty(PropertyName = "message")]
        public Message message { get; set; }
        [JsonProperty(PropertyName = "more_replies")]
        public bool more_replies { get; set; }
    }
}