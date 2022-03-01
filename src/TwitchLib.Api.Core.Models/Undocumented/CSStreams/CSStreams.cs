using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.CSStreams
{
    public class CSStreams
    {
        [JsonPropertyName("_total")]
        public int Total { get; protected set; }
        [JsonPropertyName("streams")]
        public CSStream[] Streams { get; protected set; }
    }
}
