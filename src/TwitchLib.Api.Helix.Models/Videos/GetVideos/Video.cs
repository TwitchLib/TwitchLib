using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Videos.GetVideos
{
    public class Video
    {
        [JsonPropertyName("created_at")]
        public string CreatedAt { get; protected set; }
        [JsonPropertyName("description")]
        public string Description { get; protected set; }
        [JsonPropertyName("duration")]
        public string Duration { get; protected set; }
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("language")]
        public string Language { get; protected set; }
        [JsonPropertyName("published_at")]
        public string PublishedAt { get; protected set; }
        [JsonPropertyName("thumbnail_url")]
        public string ThumbnailUrl { get; protected set; }
        [JsonPropertyName("title")]
        public string Title { get; protected set; }
        [JsonPropertyName("user_id")]
        public string UserId { get; protected set; }
        [JsonPropertyName("user_login")]
        public string UserLogin { get; protected set; }
        [JsonPropertyName("user_name")]
        public string UserName { get; protected set; }
        [JsonPropertyName("view_count")]
        public int ViewCount { get; protected set; }
        [JsonPropertyName("stream_id")]
        public string StreamId { get; protected set; }
        [JsonPropertyName("muted_segments")]
        public MutedSegment[] MutedSegments { get; protected set; }
    }
}
