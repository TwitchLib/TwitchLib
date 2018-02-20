using Newtonsoft.Json;

namespace TwitchLib.Models.API.Helix.StreamsMetadata
{
    public class PlayerHearthstone
    {
        [JsonProperty(PropertyName = "hero")]
        public HeroHearthstone Hero { get; protected set; }
    }
}
