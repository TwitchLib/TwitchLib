using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v3.Follows
{
    public class FollowsResponse
    {
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
        [JsonProperty(PropertyName = "follows")]
        public Follows[] Follows { get; protected set; }
    }
}
