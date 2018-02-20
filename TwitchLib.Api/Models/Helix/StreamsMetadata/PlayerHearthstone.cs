using Newtonsoft.Json;

namespace TwitchLib.Api.Models.Helix.StreamsMetadata
{
    public class PlayerHearthstone
    {
        [JsonProperty(PropertyName = "hero")]
        public HeroHearthstone Hero { get; protected set; }
    }
}
