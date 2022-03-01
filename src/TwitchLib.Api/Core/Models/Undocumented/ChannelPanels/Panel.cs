using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.ChannelPanels
{
    public class Panel
    {
        [JsonPropertyName("_id")]
        public string Id { get; protected set; }
        [JsonPropertyName("display_order")]
        public int DisplayOrder { get; protected set; }
        [JsonPropertyName("default")]
        public string Kind { get; protected set; }
        [JsonPropertyName("html_description")]
        public string HtmlDescription { get; protected set; }
        [JsonPropertyName("user_id")]
        public string UserId { get; protected set; }
        [JsonPropertyName("data")]
        public Data Data { get; protected set; }
        [JsonPropertyName("channel")]
        public string Channel { get; protected set; }
    }
}
