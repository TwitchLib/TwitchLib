using Newtonsoft.Json;
using System;

namespace TwitchLib.Models.API.Helix.Users.GetUsersFollows
{
    public class Follow
    {
        [JsonProperty(PropertyName = "from_id")]
        public string FromUserId { get; protected set; }
        [JsonProperty(PropertyName = "to_id")]
        public string ToUserId { get; protected set; }
        [JsonProperty(PropertyName = "followed_at")]
        public DateTime FollowedAt { get; protected set; }
    }
}
