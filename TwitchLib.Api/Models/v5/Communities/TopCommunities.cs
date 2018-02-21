using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v5.Communities
{
    public class TopCommunities
    {
        #region Cursor
        [JsonProperty(PropertyName = "_cursor")]
        public string Cursor { get; protected set; }
        #endregion
        #region Total
        [JsonProperty(PropertyName = "_total")]
        public int Total{ get; protected set; }
        #endregion
        #region Communities
        [JsonProperty(PropertyName = "communities")]
        public TopCommunity[] Communities { get; protected set; }
        #endregion
    }
}
