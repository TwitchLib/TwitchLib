using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Root
{
    public class Authorization
    {
        [JsonProperty(PropertyName = "username")]
        public string Username { get; protected set; }
        [JsonProperty(PropertyName = "scopes")]
        public string[] Scopes { get; protected set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; protected set; }
        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedAt { get; protected set; }
    }
}
