using Newtonsoft.Json;

namespace TwitchLib.Models.API.Undocumented.Comments
{
    public class Emoticons
    {
        [JsonProperty(PropertyName = "_id")]
        public string _id { get; set; }
        [JsonProperty(PropertyName = "begin")]
        public int begin { get; set; }
        [JsonProperty(PropertyName = "end")]
        public int end { get; set; }
    }
}