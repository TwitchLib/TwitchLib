using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Clips.GetClips
{
    public class Clip
    {
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("url")]
        public string Url { get; protected set; }
        [JsonPropertyName("embed_url")]
        public string EmbedUrl { get; protected set; }
        [JsonPropertyName("broadcaster_id")]
        public string BroadcasterId { get; protected set; }
        [JsonPropertyName("creator_id")]
        public string CreatorId { get; protected set; }
        [JsonPropertyName("video_id")]
        public string VideoId { get; protected set; }
        [JsonPropertyName("game_id")]
        public string GameId { get; protected set; }
        [JsonPropertyName("language")]
        public string Language { get; protected set; }
        [JsonPropertyName("title")]
        public string Title { get; protected set; }
        [JsonPropertyName("view_count")]
        public int ViewCount { get; protected set; }
        [JsonPropertyName("created_at")]
        public string CreatedAt { get; protected set; }
        [JsonPropertyName("thumbnail_url")]
        public string ThumbnailUrl { get; protected set; }
        [JsonPropertyName("duration")]
        public float Duration { get; protected set; }
    }
}
