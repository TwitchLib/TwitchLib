using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.ChannelExtensionData
{
    public class Views
    {
        [JsonPropertyName("video_overlay")]
        public View VideoOverlay { get; protected set; }
        [JsonPropertyName("config")]
        public View Config { get; protected set; }
    }
}
