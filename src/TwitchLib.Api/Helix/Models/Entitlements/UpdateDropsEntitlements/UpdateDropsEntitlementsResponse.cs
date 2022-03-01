using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Entitlements.UpdateDropsEntitlements
{
    public class UpdateDropsEntitlementsResponse
    {
        [JsonPropertyName("data")]
        public DropEntitlementUpdate[] DropEntitlementUpdates { get; protected set; }
    }
}