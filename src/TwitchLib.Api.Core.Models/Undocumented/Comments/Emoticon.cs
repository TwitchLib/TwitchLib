using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.Comments
{
    public class Emoticon
    {
        [JsonPropertyName("emoticon_id")]
        public string EmoticonId { get; set; }
        [JsonPropertyName("emoticon_set_id")]
        public string EmoticonSetId { get; set; }
    }
}