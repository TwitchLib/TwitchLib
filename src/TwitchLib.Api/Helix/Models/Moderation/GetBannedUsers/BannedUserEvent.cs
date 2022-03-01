using System.Text.Json.Serialization;
using System;

namespace TwitchLib.Api.Helix.Models.Moderation.GetBannedUsers
{
    public class BannedUserEvent
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; protected set; }
        [JsonPropertyName("user_login")]
        public string UserLogin { get; protected set; }
        [JsonPropertyName("user_name")]
        public string UserName { get; protected set; }
        [JsonPropertyName("expires_at")]
        public DateTime? ExpiresAt { get; protected set; }
        [JsonPropertyName("reason")]
        public string Reason { get; protected set; }
        [JsonPropertyName("moderator_id")]
        public string ModeratorId { get; protected set; }
        [JsonPropertyName("moderator_login")]
        public string ModeratorLogin { get; protected set; }
        [JsonPropertyName("moderator_name")]
        public string ModeratorName { get; protected set; }
    }
}
