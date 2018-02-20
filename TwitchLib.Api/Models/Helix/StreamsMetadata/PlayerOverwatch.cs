using Newtonsoft.Json;

namespace TwitchLib.Api.Models.Helix.StreamsMetadata
{
    public class PlayerOverwatch
    {
        [JsonProperty(PropertyName = "hero")]
        public HeroOverwatch Hero { get; protected set; }
    }
}
