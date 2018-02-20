using Newtonsoft.Json;

namespace TwitchLib.Models.API.v3.Chat
{
    public class EmoticonImage
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "code")]
        public string Code { get; protected set; }
    }
}
