using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Analytics
{
    public class GetGameAnalyticsResponse
    {
        [JsonPropertyName("data")]
        public GameAnalytics[] Data { get; protected set; }
        [JsonPropertyName("pagination")]
        public Common.Pagination Pagination { get; protected set; }
    }
}
