using System;
using Newtonsoft.Json;

namespace TwitchLib.Models.API.Undocumented.Comments
{
    public class Commenter
    {
        [JsonProperty(PropertyName = "display_name")]
        public string display_name { get; set; }
        [JsonProperty(PropertyName = "_id")]
        public string _id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string type { get; set; }
        [JsonProperty(PropertyName = "bio")]
        public string bio { get; set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime created_at { get; set; }
        [JsonProperty(PropertyName = "updated_at")]
        public DateTime updated_at { get; set; }
        [JsonProperty(PropertyName = "logo")]
        public string logo { get; set; }
    }
}