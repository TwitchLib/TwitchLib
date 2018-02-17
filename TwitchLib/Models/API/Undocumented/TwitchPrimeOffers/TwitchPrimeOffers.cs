using Newtonsoft.Json;

namespace TwitchLib.Models.API.Undocumented.TwitchPrimeOffers
{
    public class TwitchPrimeOffers
    {
        [JsonProperty(PropertyName = "offers")]
        public Offer[] Offers { get; protected set; }
    }
}
