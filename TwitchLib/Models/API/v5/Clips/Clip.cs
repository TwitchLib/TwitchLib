using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v5.Clips
{
    public class Clip
    {
        [JsonProperty(PropertyName = "broadcaster")]
        public Broadcaster Broadcaster { get; protected set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; protected set; }
        [JsonProperty(PropertyName = "curator")]
        public Curator Curator { get; protected set; }
        [JsonProperty(PropertyName = "duration")]
        public double Duration { get; protected set; }
        [JsonProperty(PropertyName = "embed_html")]
        public string EmbedHtml { get; protected set; }
        [JsonProperty(PropertyName = "embed_url")]
        public string EmbedUrl { get; protected set; }
        [JsonProperty(PropertyName = "game")]
        public string Game { get; protected set; }
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "language")]
        public string Language { get; protected set; }
        [JsonProperty(PropertyName = "thumbnails")]
        public Thumbnails Thumbnails { get; protected set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }
        [JsonProperty(PropertyName = "tracking_id")]
        public string TrackingId { get; protected set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; protected set; }
        [JsonProperty(PropertyName = "views")]
        public int Views { get; protected set; }
        [JsonProperty(PropertyName = "vod")]
        public VOD VOD { get; protected set; }
    }
}
