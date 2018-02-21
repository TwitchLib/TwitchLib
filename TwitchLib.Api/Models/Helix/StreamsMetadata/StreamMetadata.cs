using Newtonsoft.Json;

namespace TwitchLib.Api.Models.Helix.StreamsMetadata
{
    public class StreamMetadata
    {
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; protected set; }
        [JsonProperty(PropertyName = "game_id")]
        public string GameId { get; protected set; }
        [JsonProperty(PropertyName = "hearthstone")]
        public Hearthstone Hearthstone { get; protected set; }
        [JsonProperty(PropertyName = "overwatch")]
        public Overwatch Overwatch { get; protected set; }
    }
}
