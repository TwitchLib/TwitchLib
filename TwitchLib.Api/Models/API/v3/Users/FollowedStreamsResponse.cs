using Newtonsoft.Json;

namespace TwitchLib.Models.API.v3.Users
{
    public class FollowedStreamsResponse
    {
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
        [JsonProperty(PropertyName = "streams")]
        public Streams.Stream[] Streams { get; protected set; }
    }
}
