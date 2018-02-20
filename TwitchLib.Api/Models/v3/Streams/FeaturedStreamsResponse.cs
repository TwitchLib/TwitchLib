using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v3.Streams
{
    public class FeaturedStreamsResponse
    {
        [JsonProperty(PropertyName = "featured")]
        public FeaturedStream[] FeaturedStreams { get; protected set; }
    }
}
