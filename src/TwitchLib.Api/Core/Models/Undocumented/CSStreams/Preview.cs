using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.CSStreams
{
    public class Preview
    {
        [JsonPropertyName("small")]
        public string Small { get; protected set; }
        [JsonPropertyName("medium")]
        public string Medium { get; protected set; }
        [JsonPropertyName("large")]
        public string Large { get; protected set; }
        [JsonPropertyName("template")]
        public string Template { get; protected set; }
    }
}
