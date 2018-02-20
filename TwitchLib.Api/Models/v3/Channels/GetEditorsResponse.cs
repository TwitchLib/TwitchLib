using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v3.Channels
{
    public class GetEditorsResponse
    {
        [JsonProperty(PropertyName = "users")]
        public Users.User[] Editors { get; protected set; }
    }
}
