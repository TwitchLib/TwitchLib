using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.Comments
{
    public class CommentsPage
    {
        [JsonPropertyName("comments")]
        public Comment[] Comments { get; set; }
        [JsonPropertyName("_prev")]
        public string Prev { get; set; }
        [JsonPropertyName("_next")]
        public string Next { get; set; }
    }
}
