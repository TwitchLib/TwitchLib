using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Search
{
    public class SearchStreams
    {
        #region Total
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
        #endregion
        #region Streams
        [JsonProperty(PropertyName = "streams")]
        public Streams.Stream[] Streams { get; protected set; }
        #endregion
    }
}
