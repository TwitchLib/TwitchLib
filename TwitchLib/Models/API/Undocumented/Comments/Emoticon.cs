using Newtonsoft.Json;

namespace TwitchLib.Models.API.Undocumented.Comments
{
    public class Emoticon
    {
        [JsonProperty(PropertyName = "emoticon_id")]
        public string emoticon_id { get; set; }
        [JsonProperty(PropertyName = "emoticon_set_id")]
        public string emoticon_set_id { get; set; }
    }
}