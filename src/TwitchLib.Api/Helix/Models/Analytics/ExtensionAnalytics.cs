using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Analytics
{
    public class ExtensionAnalytics
    {
        [JsonPropertyName("extension_id")]
        public string ExtensionId { get; protected set; }
        [JsonPropertyName("URL")]
        public string Url { get; protected set; }
    }
}
