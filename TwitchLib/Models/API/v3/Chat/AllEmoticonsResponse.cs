using Newtonsoft.Json;

namespace TwitchLib.Models.API.v3.Chat
{
    public class AllEmoticonsResponse
    {
        [JsonProperty(PropertyName = "emoticons")]
        public Emoticon[] Emoticons { get; protected set; }
    }
}
