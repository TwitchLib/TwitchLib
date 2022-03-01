using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.ChannelExtensionData
{
    public class InstalledExtension
    {
        [JsonPropertyName("extension")]
        public Extension Extension { get; protected set; }
        [JsonPropertyName("installation_status")]
        public InstallationStatus InstallationStatus { get; protected set; }
    }
}
