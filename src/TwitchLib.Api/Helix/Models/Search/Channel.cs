using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace TwitchLib.Api.Helix.Models.Search
{
    public class Channel
    {
        [JsonPropertyName("game_id")]
        public string GameId { get; protected set; }
        [JsonPropertyName("game_name")]
        public string GameName { get; protected set; }
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("broadcaster_login")]
        public string BroadcasterLogin { get; protected set; }
        [JsonPropertyName("display_name")]
        public string DisplayName { get; protected set; }
        [JsonPropertyName("broadcaster_language")]
        public string BroadcasterLanguage { get; protected set; }
        [JsonPropertyName("title")]
        public string Title { get; protected set; }
        [JsonPropertyName("thumbnail_url")]
        public string ThumbnailUrl { get; protected set; }
        [JsonPropertyName("is_live")]
        public bool IsLive { get; protected set; }
        [JsonPropertyName("started_at")]
        public DateTime? StartedAt { get; protected set; }
        [JsonPropertyName("tag_ids")]
        public List<string> TagIds { get; protected set; }
    }
}
