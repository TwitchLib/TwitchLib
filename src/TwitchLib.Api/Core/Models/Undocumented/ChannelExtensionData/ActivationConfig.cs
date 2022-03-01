using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.ChannelExtensionData
{
    public class ActivationConfig
    {
        [JsonPropertyName("slot")]
        public string Slot { get; protected set; }
        [JsonPropertyName("anchor")]
        public string Anchor { get; protected set; }
    }
}
