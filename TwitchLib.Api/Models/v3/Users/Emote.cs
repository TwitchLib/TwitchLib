using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v3.Users
{
    public class Emote
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; protected set; }
        [JsonProperty(PropertyName = "code")]
        public string Code { get; protected set; }
    }
}
