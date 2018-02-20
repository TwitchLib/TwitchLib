using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v3.Blocks
{
    public class Block
    {
        [JsonProperty(PropertyName = "updated_at")]
        public string UpdatedAt { get; protected set; }
        [JsonProperty(PropertyName = "user")]
        public Users.User User { get; protected set; }
        [JsonProperty(PropertyName = "_id")]
        public int Id { get; protected set; }
    }
}
