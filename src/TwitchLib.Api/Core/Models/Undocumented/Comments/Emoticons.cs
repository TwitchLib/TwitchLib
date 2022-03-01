using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.Comments
{
    public class Emoticons
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }
        [JsonPropertyName("begin")]
        public int Begin { get; set; }
        [JsonPropertyName("end")]
        public int End { get; set; }
    }
}