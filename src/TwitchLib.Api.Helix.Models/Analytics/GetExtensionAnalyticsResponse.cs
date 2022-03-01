using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Analytics
{
    public class GetExtensionAnalyticsResponse
    {
        [JsonPropertyName("data")]
        public ExtensionAnalytics[] Data { get; protected set; }
    }
}
