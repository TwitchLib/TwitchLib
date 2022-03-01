using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.ChannelExtensionData
{
    public class IconUrls
    {
        [JsonPropertyName("100x100")]
        public string Url100x100 { get; protected set; }
    }
}
