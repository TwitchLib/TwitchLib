using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Entitlements.GetCodeStatus
{
    public class GetCodeStatusResponse
    {
        [JsonPropertyName("data")]
        public Status[] Data { get; protected set; }
    }
}
