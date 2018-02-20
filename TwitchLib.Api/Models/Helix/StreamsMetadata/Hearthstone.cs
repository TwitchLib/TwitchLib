using Newtonsoft.Json;

namespace TwitchLib.Api.Models.Helix.StreamsMetadata
{
    public class Hearthstone
    {
        [JsonProperty(PropertyName = "broadcaster")]
        public PlayerHearthstone Broadcaster { get; protected set; }
        [JsonProperty(PropertyName = "opponent")]
        public PlayerHearthstone Opponent { get; protected set; }
    }
}
