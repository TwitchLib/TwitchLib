using Newtonsoft.Json;

namespace TwitchLib.Models.API.Undocumented.Comments
{
    public class Message
    {
        [JsonProperty(PropertyName = "body")]
        public string body { get; set; }
        [JsonProperty(PropertyName = "emoticons")]
        public Emoticons[] emoticons { get; set; }
        [JsonProperty(PropertyName = "fragments")]
        public Fragment[] fragments { get; set; }
        [JsonProperty(PropertyName = "is_action")]
        public bool is_action { get; set; }
        [JsonProperty(PropertyName = "user_color")]
        public string user_color { get; set; }
        [JsonProperty(PropertyName = "user_badges")]
        public User_Badges[] user_badges { get; set; }
    }
}