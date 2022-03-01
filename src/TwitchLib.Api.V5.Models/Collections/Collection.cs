using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Collections
{
    public class Collection
    {
        #region Id
        /// <summary>Property representing the Collection Id.</summary>
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; protected set; }
        #endregion
        #region Items
        /// <summary>Property representing the Collection items.</summary>
        [JsonProperty(PropertyName = "items")]
        public CollectionItem[] Items { get; protected set; }
        #endregion
    }
}
