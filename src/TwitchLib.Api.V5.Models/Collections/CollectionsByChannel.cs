using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Collections
{
    public class CollectionsByChannel
    {
        #region Cursor
        [JsonProperty(PropertyName = "_cursor")]
        public string Cursor { get; protected set; }
        #endregion
        #region Collections
        [JsonProperty(PropertyName = "collections")]
        public CollectionMetadata[] Collections { get; protected set; }
        #endregion
    }
}
