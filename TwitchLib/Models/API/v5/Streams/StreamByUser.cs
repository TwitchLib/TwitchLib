namespace TwitchLib.Models.API.v5.Streams
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class StreamByUser
    {
        #region Stream
        [JsonProperty(PropertyName = "stream")]
        public Stream Stream { get; protected set; }
        #endregion
    }
}
