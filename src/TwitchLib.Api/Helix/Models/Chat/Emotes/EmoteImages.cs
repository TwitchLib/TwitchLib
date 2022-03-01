using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes
{
    public class EmoteImages
    {
        [JsonPropertyName("url_1x")]
        public string Url1X { get; protected set; }
        [JsonPropertyName("url_2x")]
        public string Url2X { get; protected set; }
        [JsonPropertyName("url_4x")]
        public string Url4X { get; protected set; }
    }
}