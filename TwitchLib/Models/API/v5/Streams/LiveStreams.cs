namespace TwitchLib.Models.API.v5.Streams
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class LiveStreams
    {
        #region Total
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
        #endregion
        #region Streams
        [JsonProperty(PropertyName = "streams")]
        public Stream[] Streams { get; protected set; }
        #endregion
    }
}
