using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Analytics
{
    public class GameAnalytics
    {
        [JsonPropertyName("game_id")]
        public string GameId { get; protected set; }
        [JsonPropertyName("URL")]
        public string Url { get; protected set; }
        [JsonPropertyName("type")]
        public string Type { get; protected set; }
        [JsonPropertyName("date_range")]
        public Common.DateRange DateRange { get; protected set; }
    }
}
