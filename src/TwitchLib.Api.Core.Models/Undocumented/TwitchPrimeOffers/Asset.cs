using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.TwitchPrimeOffers
{
    public class Asset
    {
        [JsonPropertyName("assetType")]
        public string AssetType { get; protected set; }
        [JsonPropertyName("location")]
        public string Location { get; protected set; }
        [JsonPropertyName("location2x")]
        public string Location2x { get; protected set; }
        [JsonPropertyName("mediaType")]
        public string MediaType { get; protected set; }
    }
}
