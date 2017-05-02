namespace TwitchLib.Models.API.Undocumented.TwitchPrimeOffers
{
    #region using directives
    using Newtonsoft.Json;
    #endregion
    public class TwitchPrimeOffers
    {
        [JsonProperty(PropertyName = "offers")]
        public Offer[] Offers { get; protected set; }
    }
}
