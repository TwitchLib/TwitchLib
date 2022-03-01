using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.ChannelExtensionData
{
    public class InstallationStatus
    {
        [JsonPropertyName("extension_id")]
        public string ExtensionId { get; protected set; }
        [JsonPropertyName("activation_config")]
        public ActivationConfig ActivationConfig { get; protected set; }
        [JsonPropertyName("activation_state")]
        public string ActivationState { get; protected set; }
        [JsonPropertyName("can_activate")]
        public bool CanActivate { get; protected set; }
    }
}
