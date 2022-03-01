using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.ChannelExtensionData
{
    public class GetChannelExtensionDataResponse
    {
        [JsonPropertyName("issued_at")]
        public string IssuedAt { get; protected set; }
        [JsonPropertyName("tokens")]
        public ExtToken[] Tokens { get; protected set; }
        [JsonPropertyName("installed_extensions")]
        public InstalledExtension[] InstalledExtensions { get; protected set; }
    }
}
