using Newtonsoft.Json;

namespace TwitchLib.Api.Models.Undocumented.Comments
{
    public class Message
    {
        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }
        [JsonProperty(PropertyName = "emoticons")]
        public Emoticons[] Emoticons { get; set; }
        [JsonProperty(PropertyName = "fragments")]
        public Fragment[] Fragments { get; set; }
        [JsonProperty(PropertyName = "is_action")]
        public bool IsAction { get; set; }
        [JsonProperty(PropertyName = "user_color")]
        public string UserColor { get; set; }
        [JsonProperty(PropertyName = "user_badges")]
        public UserBadges[] UserBadges { get; set; }
    }
}