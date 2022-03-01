using System.Text.Json.Serialization;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Helix.Models.Entitlements.UpdateDropsEntitlements
{
    public class DropEntitlementUpdate
    {
        [JsonPropertyName("status")]
        public DropEntitlementUpdateStatus Status { get; protected set; }
        [JsonPropertyName("ids")]
        public string[] Ids { get; protected set; }
    }
}