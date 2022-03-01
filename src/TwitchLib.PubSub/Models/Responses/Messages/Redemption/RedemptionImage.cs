using System.Text.Json.Serialization;

namespace TwitchLib.PubSub.Models.Responses.Messages.Redemption
{
    public class RedemptionImage
    {
        [JsonPropertyName("url_1x")]
        public string Url1x { get; protected set; }
        [JsonPropertyName("url_2x")]
        public string Url2x { get; protected set; }
        [JsonPropertyName("url_4x")]
        public string Url4x { get; protected set; }
    }
}
