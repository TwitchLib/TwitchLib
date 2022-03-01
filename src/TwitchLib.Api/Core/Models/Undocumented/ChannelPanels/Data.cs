using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.ChannelPanels
{
    public class Data
    {
        [JsonPropertyName("link")]
        public string Link { get; protected set; }
        [JsonPropertyName("image")]
        public string Image { get; protected set; }
        [JsonPropertyName("title")]
        public string Title { get; protected set; }
    }
}
