using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.Comments
{
    public class Fragment
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("emoticon")]
        public Emoticon Emoticon { get; set; }
    }
}