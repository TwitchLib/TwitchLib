using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.ChannelExtensionData
{
    public class ExtToken
    {
        [JsonPropertyName("extension_id")]
        public string ExtensionId { get; protected set; }
        [JsonPropertyName("token")]
        public string Token { get; protected set; }
    }
}
