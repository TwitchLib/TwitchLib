using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.TwitchPrimeOffers
{
    public class TwitchPrimeOffers
    {
        [JsonPropertyName("offers")]
        public Offer[] Offers { get; protected set; }
    }
}
