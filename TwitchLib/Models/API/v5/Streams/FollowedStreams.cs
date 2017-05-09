namespace TwitchLib.Models.API.v5.Streams
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
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
