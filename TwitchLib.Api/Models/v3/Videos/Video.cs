using Newtonsoft.Json;
using System;

namespace TwitchLib.Api.Models.v3.Videos
{
    public class Video
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; protected set; }
        [JsonProperty(PropertyName = "broadcast_id")]
        public string BroadcastId { get; protected set; }
        [JsonProperty(PropertyName = "status")]
        public string Status { get; protected set; }
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "tag_list")]
        public string TagList { get; protected set; }
        [JsonProperty(PropertyName = "recorded_at")]
        public DateTime RecordedAt { get; protected set; }
        [JsonProperty(PropertyName = "game")]
        public string Game { get; protected set; }
        [JsonProperty(PropertyName = "length")]
        public int Length { get; protected set; }
        [JsonProperty(PropertyName = "preview")]
        public string Preview { get; protected set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; protected set; }
        [JsonProperty(PropertyName = "views")]
        public int Views { get; protected set; }
        [JsonProperty(PropertyName = "broadcast_type")]
        public string BroadcastType { get; protected set; }
        [JsonProperty(PropertyName = "channel")]
        public Channel Channel { get; protected set; }
    }
}
