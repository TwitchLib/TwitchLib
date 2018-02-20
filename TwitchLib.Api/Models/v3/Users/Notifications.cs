using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v3.Users
{
    public class Notifications
    {
        [JsonProperty(PropertyName = "email")]
        public bool Email { get; protected set; }
        [JsonProperty(PropertyName = "push")]
        public bool Push { get; protected set; }
    }
}
