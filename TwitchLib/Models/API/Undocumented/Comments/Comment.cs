namespace TwitchLib.Models.API.Undocumented.Comments
{
    public class Comment
    {
        public string _id { get; set; }
        public object created_at { get; set; }
        public object updated_at { get; set; }
        public string channel_id { get; set; }
        public string content_type { get; set; }
        public string content_id { get; set; }
        public float content_offset_seconds { get; set; }
        public Commenter commenter { get; set; }
        public string source { get; set; }
        public string state { get; set; }
        public Message message { get; set; }
        public bool more_replies { get; set; }
    }
}