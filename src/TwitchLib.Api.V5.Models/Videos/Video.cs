using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Videos
{
    public class Video
    {
        #region Id
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; protected set; }
        #endregion
        #region AnimatedPreviewUrl
        [JsonProperty(PropertyName = "animated_preview_url")]
        public string AnimatedPreviewUrl { get; protected set; }
        #endregion
        #region BroadcastId
        [JsonProperty(PropertyName = "broadcast_id")]
        public string BroadcastId { get; protected set; }
        #endregion
        #region BroadcastType
        [JsonProperty(PropertyName = "broadcast_type")]
        public string BroadcastType { get; protected set; }
        #endregion
        #region Channel
        [JsonProperty(PropertyName = "channel")]
        public VideoChannel Channel { get; protected set; }
        #endregion
        #region CreatedAt
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; protected set; }
        #endregion
        #region Description
        [JsonProperty(PropertyName = "description")]
        public string Description { get; protected set; }
        #endregion
        #region DescriptionHtml
        [JsonProperty(PropertyName = "description_html")]
        public string DescriptionHtml { get; protected set; }
        #endregion
        #region Fps
        [JsonProperty(PropertyName = "fps")]
        public Dictionary<string, double> Fps { get; protected set; }
        #endregion
        #region Game
        [JsonProperty(PropertyName = "game")]
        public string Game { get; protected set; }
        #endregion
        #region Language
        [JsonProperty(PropertyName = "language")]
        public string Language { get; protected set; }
        #endregion
        #region Length
        [JsonProperty(PropertyName = "length")]
        public long Length { get; protected set; }
        #endregion
        #region MutedSegments
        [JsonProperty(PropertyName = "muted_segments")]
        public VideoMutedSegment[] MutedSegments { get; protected set; }
        #endregion
        #region Preview
        [JsonProperty(PropertyName = "preview")]
        public VideoPreview Preview { get; protected set; }
        #endregion
        #region PublishedAt
        [JsonProperty(PropertyName = "published_at")]
        public DateTime PublishedAt { get; protected set; }
        #endregion
        #region RecordedAt
        [JsonProperty(PropertyName = "recorded_at")]
        public DateTime RecordedAt { get; protected set; }
        #endregion
        #region Resolutions
        [JsonProperty(PropertyName = "resolutions")]
        public Dictionary<string, string> Resolutions { get; protected set; }
        #endregion
        #region Status
        [JsonProperty(PropertyName = "status")]
        public string Status { get; protected set; }
        #endregion
        #region TagList
        [JsonProperty(PropertyName = "tag_list")]
        public string TagList { get; protected set; }
        #endregion
        #region Thumbnails
        [JsonProperty(PropertyName = "thumbnails")]
        public VideoThumbnails Thumbnails { get; protected set; }
        #endregion
        #region Title
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }
        #endregion
        #region Url
        [JsonProperty(PropertyName = "url")]
        public string Url { get; protected set; }
        #endregion
        #region Viewable
        [JsonProperty(PropertyName = "viewable")]
        public string Viewable { get; protected set; }
        #endregion
        #region ViewableAt
        [JsonProperty(PropertyName = "viewable_at")]
        public DateTime ViewableAt { get; protected set; }
        #endregion
        #region Views
        [JsonProperty(PropertyName = "views")]
        public int Views { get; protected set; }
        #endregion
    }
}
