using System.Text.Json.Serialization;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Helix.Models.Entitlements
{
    public class Status
    {
        [JsonPropertyName("code")]
        public string Code { get; protected set; }
        [JsonPropertyName("status")]
        public CodeStatusEnum StatusEnum { get; protected set; }
    }
}
