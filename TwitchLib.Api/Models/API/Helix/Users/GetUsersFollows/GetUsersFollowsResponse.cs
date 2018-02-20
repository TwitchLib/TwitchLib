using Newtonsoft.Json;
using TwitchLib.Models.API.Helix.Common;

namespace TwitchLib.Models.API.Helix.Users.GetUsersFollows
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
