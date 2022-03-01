using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Channels.GetChannelInformation
{
    public class ChannelInformation
    {
        [JsonPropertyName("broadcaster_id")]
        public string BroadcasterId { get; protected set; }
        [JsonPropertyName("broadcaster_name")]
        public string BroadcasterName { get; protected set; }
        [JsonPropertyName("broadcaster_language")]
        public string BroadcasterLanguage { get; protected set; }
        [JsonPropertyName("game_id")]
        public string GameId { get; protected set; }
        [JsonPropertyName("game_name")]
        public string GameName { get; protected set; }
        [JsonPropertyName("title")]
        public string Title { get; protected set; }
        [JsonPropertyName("delay")]
        public int Delay { get; protected set; }
    }
}
