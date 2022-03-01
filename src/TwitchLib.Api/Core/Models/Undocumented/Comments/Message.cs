using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.Comments
{
    public class Message
    {
        [JsonPropertyName("body")]
        public string Body { get; set; }
        [JsonPropertyName("emoticons")]
        public Emoticons[] Emoticons { get; set; }
        [JsonPropertyName("fragments")]
        public Fragment[] Fragments { get; set; }
        [JsonPropertyName("is_action")]
        public bool IsAction { get; set; }
        [JsonPropertyName("user_color")]
        public string UserColor { get; set; }
        [JsonPropertyName("user_badges")]
        public UserBadges[] UserBadges { get; set; }
    }
}