using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Streams
{
    public class FollowedStreams
    {
        #region Total
        /// <summary>Property representing the followed Streams count.</summary>
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
        #endregion
        #region Streams
        /// <summary>Property representing the followed Streams.</summary>
        [JsonProperty(PropertyName = "streams")]
        public Stream[] Streams { get; protected set; }
        #endregion
    }
}
