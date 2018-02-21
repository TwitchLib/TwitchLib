using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v5.Streams
{
    public class StreamsSummary
    {
        #region Channels
        [JsonProperty(PropertyName = "channels")]
        public int Channels { get; protected set; }
        #endregion
        #region Viewers
        [JsonProperty(PropertyName = "viewers")]
        public int Viewers { get; protected set; }
        #endregion
    }
}
