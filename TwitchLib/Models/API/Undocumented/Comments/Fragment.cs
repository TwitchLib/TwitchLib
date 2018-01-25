using Newtonsoft.Json;

namespace TwitchLib.Models.API.Undocumented.Comments
{
    public class Fragment
    {
        [JsonProperty(PropertyName = "text")]
        public string text { get; set; }
        [JsonProperty(PropertyName = "emoticon")]
        public Emoticon emoticon { get; set; }
    }
}