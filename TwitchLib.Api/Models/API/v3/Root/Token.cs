using Newtonsoft.Json;

namespace TwitchLib.Models.API.v3.Root
{
    public class Token
    {
        [JsonProperty(PropertyName = "valid")]
        public bool Valid { get; protected set; }
        [JsonProperty(PropertyName = "authorization", NullValueHandling = NullValueHandling.Ignore)]
        public Authorization Authorization { get; protected set; }
        [JsonProperty(PropertyName = "user_name")]
        public string Username { get; protected set; }
        [JsonProperty(PropertyName = "client_id")]
        public string ClientId { get; protected set; }
    }
}
