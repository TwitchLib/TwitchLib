using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v3.Chat
{
    public class Emoticon
    {
        [JsonProperty(PropertyName = "regex")]
        public string Regex { get; protected set; }
        [JsonProperty(PropertyName = "images")]
        public Image[] Images { get; protected set; }
    }
}
