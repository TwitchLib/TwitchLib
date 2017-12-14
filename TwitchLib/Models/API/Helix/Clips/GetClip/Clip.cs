using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Models.API.Helix.Clips.GetClip
{
    public class Clip
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; protected set; }
        [JsonProperty(PropertyName = "embed_url")]
        public string EmbedUrl { get; protected set; }
        [JsonProperty(PropertyName = "broadcaster_id")]
        public string BroadcasterId { get; protected set; }
        [JsonProperty(PropertyName = "creator_id")]
        public string CreatorId { get; protected set; }
        [JsonProperty(PropertyName = "video_id")]
        public string VideoId { get; protected set; }
        [JsonProperty(PropertyName = "gane_id")]
        public string GameId { get; protected set; }
        [JsonProperty(PropertyName = "language")]
        public string Language { get; protected set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }
        [JsonProperty(PropertyName = "view_count")]
        public int ViewCount { get; protected set; }
        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; protected set; }
        [JsonProperty(PropertyName = "thumbnail_url")]
        public string ThumbnailUrl { get; protected set; }
    }
}
