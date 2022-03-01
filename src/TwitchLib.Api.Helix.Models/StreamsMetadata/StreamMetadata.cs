using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.StreamsMetadata
{
    public class StreamMetadata
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; protected set; }
        [JsonPropertyName("game_id")]
        public string GameId { get; protected set; }
        [JsonPropertyName("hearthstone")]
        public Hearthstone Hearthstone { get; protected set; }
        [JsonPropertyName("overwatch")]
        public Overwatch Overwatch { get; protected set; }
    }
}
