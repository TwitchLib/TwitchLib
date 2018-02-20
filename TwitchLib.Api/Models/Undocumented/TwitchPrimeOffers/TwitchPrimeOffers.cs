using Newtonsoft.Json;

namespace TwitchLib.Api.Models.Undocumented.TwitchPrimeOffers
{
    public class TwitchPrimeOffers
    {
        [JsonProperty(PropertyName = "offers")]
        public Offer[] Offers { get; protected set; }
    }
}
