using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.ChannelExtensionData
{
    public class ViewerUrls
    {
        [JsonPropertyName("video_overlay")]
        public string VideoOverlay { get; protected set; }
    }
}
