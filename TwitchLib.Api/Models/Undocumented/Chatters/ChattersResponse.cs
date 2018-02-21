using Newtonsoft.Json;

namespace TwitchLib.Api.Models.Undocumented.Chatters
{
    public class ChattersResponse
    {
        [JsonProperty(PropertyName = "chatter_count")]
        public int ChatterCount { get; protected set; }
        [JsonProperty(PropertyName = "chatters")]
        public Chatters Chatters { get; protected set; }
    }
}
