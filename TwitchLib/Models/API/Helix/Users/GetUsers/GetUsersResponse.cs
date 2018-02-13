using Newtonsoft.Json;

namespace TwitchLib.Models.API.Helix.Users.GetUsers
{
    public class GetUsersResponse
    {
        [JsonProperty(PropertyName = "data")]
        public User[] Users { get; protected set; }
    }
}
