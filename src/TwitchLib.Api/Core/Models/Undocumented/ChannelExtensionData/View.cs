using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.ChannelExtensionData
{
    public class View
    {
        [JsonPropertyName("viewer_url")]
        public string ViewerUrl { get; protected set; }
    }
}
