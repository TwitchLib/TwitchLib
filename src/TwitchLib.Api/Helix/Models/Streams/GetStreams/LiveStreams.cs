using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreams
{
    public class LiveStreams
    {
        #region Total
        [JsonPropertyName("_total")]
        public int Total { get; protected set; }
        #endregion
        #region Streams
        [JsonPropertyName("streams")]
        public Stream[] Streams { get; protected set; }
        #endregion
    }
}
