using Newtonsoft.Json;
using TwitchLib.Api.Models.Helix.Common;

namespace TwitchLib.Api.Models.Helix.Users.GetUsersFollows
{
    public class GetUsersFollowsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Follow[] Follows { get; protected set; }
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
        [JsonProperty(PropertyName = "total")]
        public long TotalFollows { get; protected set; }
    }
}
