using System.Text.Json.Serialization;
using System;

namespace TwitchLib.Api.Helix.Models.Users.GetUsers
{
    public class User
    {
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("login")]
        public string Login { get; protected set; }
        [JsonPropertyName("display_name")]
        public string DisplayName { get; protected set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; protected set; }
        [JsonPropertyName("type")]
        public string Type { get; protected set; }
        [JsonPropertyName("broadcaster_type")]
        public string BroadcasterType { get; protected set; }
        [JsonPropertyName("description")]
        public string Description { get; protected set; }
        [JsonPropertyName("profile_image_url")]
        public string ProfileImageUrl { get; protected set; }
        [JsonPropertyName("offline_image_url")]
        public string OfflineImageUrl { get; protected set; }
        [JsonPropertyName("view_count")]
        public long ViewCount { get; protected set; }
        [JsonPropertyName("email")]
        public string Email { get; protected set; }
    }
}
