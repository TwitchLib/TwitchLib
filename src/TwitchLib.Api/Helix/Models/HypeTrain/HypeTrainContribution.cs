using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.HypeTrain
{
    public class HypeTrainContribution
    {
        [JsonPropertyName("total")]
        public int Total { get; protected set; }
        [JsonPropertyName("type")]
        public string Type { get; protected set; }
        [JsonPropertyName("user")]
        public string UserId { get; protected set; }
    }
}