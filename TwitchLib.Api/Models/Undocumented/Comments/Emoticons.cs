using Newtonsoft.Json;

namespace TwitchLib.Api.Models.Undocumented.Comments
{
    public class Emoticons
    {
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "begin")]
        public int Begin { get; set; }
        [JsonProperty(PropertyName = "end")]
        public int End { get; set; }
    }
}