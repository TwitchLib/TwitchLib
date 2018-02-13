using Newtonsoft.Json;
using System;

namespace TwitchLib.Models.API.v3.Teams
{
    public class Team
    {
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "info")]
        public string Info { get; set; }
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty(PropertyName = "logo")]
        public string Logo { get; set; }
        [JsonProperty(PropertyName = "banner")]
        public string Banner { get; set; }
        [JsonProperty(PropertyName = "background")]
        public string Background { get; set; }
    }
}
