using System.Text.Json.Serialization;
using System;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreams
{
    public class Stream
    {
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("user_id")]
        public string UserId { get; protected set; }
        [JsonPropertyName("user_login")]
        public string UserLogin { get; protected set; }
        [JsonPropertyName("user_name")]
        public string UserName { get; protected set; }
        [JsonPropertyName("game_id")]
        public string GameId { get; protected set; }
        [JsonPropertyName("game_name")]
        public string GameName { get; protected set; }
        [JsonPropertyName("community_ids")]
        public string[] CommunityIds { get; protected set; }
        [JsonPropertyName("type")]
        public string Type { get; protected set; }
        [JsonPropertyName("title")]
        public string Title { get; protected set; }
        [JsonPropertyName("viewer_count")]
        public int ViewerCount { get; protected set; }
        [JsonPropertyName("started_at")]
        public DateTime StartedAt { get; protected set; }
        [JsonPropertyName("language")]
        public string Language { get; protected set; }
        [JsonPropertyName("thumbnail_url")]
        public string ThumbnailUrl { get; protected set; }
        [JsonPropertyName("is_mature")]
        public bool IsMature { get; protected set; }
    }
}
