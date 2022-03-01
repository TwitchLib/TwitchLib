using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Entitlements.RedeemCode
{
    public class RedeemCodeResponse
    {
        [JsonPropertyName("data")]
        public Status[] Data { get; protected set; }
    }
}
