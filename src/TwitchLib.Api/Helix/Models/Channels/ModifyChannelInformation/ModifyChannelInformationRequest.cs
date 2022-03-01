using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Channels.ModifyChannelInformation
{
    public class ModifyChannelInformationRequest
    {
        [JsonPropertyName("game_id")]
        public string GameId { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("broadcaster_language")]
        public string BroadcasterLanguage { get; set; }
        [JsonPropertyName("delay")]
        public int? Delay { get; set; }
    }
}
