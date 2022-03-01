using System;
using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Users.GetUserFollows
{
    public class Follow
    {
        [JsonPropertyName("from_id")]
        public string FromUserId { get; protected set; }
        [JsonPropertyName("from_name")]
        public string FromUserName { get; protected set; }
        [JsonPropertyName("to_id")]
        public string ToUserId { get; protected set; }
        [JsonPropertyName("to_name")]
        public string ToUserName { get; protected set; }
        [JsonPropertyName("followed_at")]
        public DateTime FollowedAt { get; protected set; }
    }
}
