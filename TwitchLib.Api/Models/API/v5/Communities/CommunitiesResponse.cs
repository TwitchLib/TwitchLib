using Newtonsoft.Json;

namespace TwitchLib.Models.API.v5.Communities
{
    public class CommunitiesResponse
    {
        [JsonProperty(PropertyName = "communities")]
        public Community[] Communities { get; protected set; }
    }
}
