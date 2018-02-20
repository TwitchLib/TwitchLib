using Newtonsoft.Json;
using System;

namespace TwitchLib.Api.Models.v3.Root
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
