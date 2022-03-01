using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.EventSub
{
    public class EventSubTransport
    {
        [JsonPropertyName("method")]
        public string Method { get; protected set; }
        [JsonPropertyName("callback")]
        public string Callback { get; protected set; }
    }
}