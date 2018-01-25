using Newtonsoft.Json;

namespace TwitchLib.Models.API.Undocumented.Comments
{
    public class User_Badges
    {
        [JsonProperty(PropertyName = "_id")]
        public string _id { get; set; }
        [JsonProperty(PropertyName = "version")]
        public string version { get; set; }
    }
}