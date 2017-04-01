using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Users
{
    public class User
    {
        [JsonProperty(PropertyName = "updated_at")]
        public string UpdatedAt;
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName;
        [JsonProperty(PropertyName = "type")]
        public string Type;
        [JsonProperty(PropertyName = "bio")]
        public string Bio;
        [JsonProperty(PropertyName = "name")]
        public string Name;
        [JsonProperty(PropertyName = "_id")]
        public string Id;
        [JsonProperty(PropertyName = "logo")]
        public string Logo;
        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt;
    }
}
