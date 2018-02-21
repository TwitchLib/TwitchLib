using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v5.Collections
{
    public class CollectionMetadata
    {
        #region Id
        /// <summary>Property representing the CollectionMetadata object Id.</summary>
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; protected set; }
        #endregion
        #region CreatedAt
        /// <summary>Property representing the date time of Collection creation.</summary>
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; protected set; }
        #endregion
        #region ItemsCount
        /// <summary>Property representing the count of items in the Collection.</summary>
        [JsonProperty(PropertyName = "items_count")]
        public int ItemsCount { get; protected set; }
        #endregion
        #region Owner
        /// <summary>Property representing the owner of the collection.</summary>
        [JsonProperty(PropertyName = "owner")]
        public Users.User Owner { get; protected set; }
        #endregion
        #region Thumbnails
        /// <summary>Property representing the thumbnails of the collection.</summary>
        [JsonProperty(PropertyName = "thumbnails")]
        public Dictionary<string,string> Thumbnails { get; protected set; }
        #endregion
        #region Title
        /// <summary>Property representing the title of the collection.</summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }
        #endregion
        #region TotalDuration
        /// <summary>Property representing the total duration of the collection in seconds.</summary>
        [JsonProperty(PropertyName = "total_duration")]
        public int TotalDuration { get; protected set; }
        #endregion
        #region UpdatedAt
        /// <summary>Property representing the date time of last collection update.</summary>
        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedAt { get; protected set; }
        #endregion
        #region Views
        /// <summary>Property representing the total views of the collection.</summary>
        [JsonProperty(PropertyName = "views")]
        public int Views { get; protected set; }
        #endregion
    }
}
