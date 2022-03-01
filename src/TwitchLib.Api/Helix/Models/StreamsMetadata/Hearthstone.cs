using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.StreamsMetadata
{
    public class Hearthstone
    {
        [JsonPropertyName("broadcaster")]
        public PlayerHearthstone Broadcaster { get; protected set; }
        [JsonPropertyName("opponent")]
        public PlayerHearthstone Opponent { get; protected set; }
    }
}
