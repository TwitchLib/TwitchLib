using Newtonsoft.Json;

namespace TwitchLib.Api.Models.Helix.Users.GetUsers
{
    public class GetUsersResponse
    {
        [JsonProperty(PropertyName = "data")]
        public User[] Users { get; protected set; }
    }
}
