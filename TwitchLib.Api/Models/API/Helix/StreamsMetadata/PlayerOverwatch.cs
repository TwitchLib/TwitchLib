using Newtonsoft.Json;

namespace TwitchLib.Models.API.Helix.StreamsMetadata
{
    public class PlayerOverwatch
    {
        [JsonProperty(PropertyName = "hero")]
        public HeroOverwatch Hero { get; protected set; }
    }
}
