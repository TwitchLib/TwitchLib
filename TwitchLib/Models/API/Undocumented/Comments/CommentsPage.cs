using Newtonsoft.Json;

namespace TwitchLib.Models.API.Undocumented.Comments
{
    public class CommentsPage
    {
        [JsonProperty(PropertyName = "comments")]
        public Comment[] comments { get; set; }
        [JsonProperty(PropertyName = "_prev")]
        public string _prev { get; set; }
        [JsonProperty(PropertyName = "_next")]
        public string _next { get; set; }
    }
}
