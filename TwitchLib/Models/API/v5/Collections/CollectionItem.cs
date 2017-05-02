namespace TwitchLib.Models.API.v5.Collections
{
    #region using directives
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    #endregion
    public class CollectionItem
    {
        #region Id
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; protected set; }
        #endregion
        #region DescriptionHtml
        [JsonProperty(PropertyName = "description_html")]
        public string DescriptionHtml { get; protected set; }
        #endregion
        #region Duration
        [JsonProperty(PropertyName = "duration")]
        public int Duration { get; protected set; }
        #endregion
        #region Game
        [JsonProperty(PropertyName = "game")]
        public string Game { get; protected set; }
        #endregion
        #region ItemId
        [JsonProperty(PropertyName = "item_id")]
        public string ItemId { get; protected set; }
        #endregion
        #region ItemType
        [JsonProperty(PropertyName = "item_type")]
        public string ItemType { get; protected set; }
        #endregion
        #region Owner
        [JsonProperty(PropertyName = "owner")]
        public Users.User Owner { get; protected set; }
        #endregion
        #region PublishedAt
        [JsonProperty(PropertyName = "published_at")]
        public DateTime PublishedAt { get; protected set; }
        #endregion
        #region Thumbnails
        [JsonProperty(PropertyName = "thumbnails")]
        public Dictionary<string, string> Thumbnails { get; protected set; }
        #endregion
        #region Title
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }
        #endregion
        #region Views
        [JsonProperty(PropertyName = "views")]
        public int Views { get; protected set; }
        #endregion
    }
}
