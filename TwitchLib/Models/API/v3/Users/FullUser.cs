using Newtonsoft.Json;
namespace TwitchLib.Models.API.v3.Users
{
    public class FullUser
    {
        [JsonProperty(PropertyName = "updated_at")]
        public string UpdatedAt { get; protected set; }
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; protected set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; protected set; }
        [JsonProperty(PropertyName = "bio")]
        public string Bio { get; protected set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "logo")]
        public string Logo { get; protected set; }
        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; protected set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; protected set; }
        [JsonProperty(PropertyName = "partnered")]
        public bool Partnered { get; protected set; }
        [JsonProperty(PropertyName = "notifications")]
        public Notifications Notifications { get; protected set; }
        
    }
}
