using System;
using Newtonsoft.Json;

namespace TwitchLib.Models.API.Undocumented.TwitchPrimeOffers
{
    public class Offer
    {
        [JsonProperty(PropertyName = "applicableGame")]
        public string ApplicableGame { get; protected set; }
        [JsonProperty(PropertyName = "assets")]
        public Asset[] Assets { get; protected set; }
        [JsonProperty(PropertyName = "contentCategories")]
        public string[] ContentCategories { get; protected set; }
        [JsonProperty(PropertyName = "contentClaimInstructions")]
        public string ContentClaimInstruction { get; protected set; }
        [JsonProperty(PropertyName = "contentDeliveryMethod")]
        public string ContentDeliveryMethod { get; protected set; }
        [JsonProperty(PropertyName = "endTime")]
        public DateTime EndTime { get; protected set; }
        [JsonProperty(PropertyName = "offerDescription")]
        public string OfferDescription { get; protected set; }
        [JsonProperty(PropertyName = "offerId")]
        public string OfferId { get; protected set; }
        [JsonProperty(PropertyName = "offerTitle")]
        public string OfferTitle { get; protected set; }
        [JsonProperty(PropertyName = "priority")]
        public int Priority { get; protected set; }
        [JsonProperty(PropertyName = "publisherName")]
        public string PublisherName { get; protected set; }
        [JsonProperty(PropertyName = "startTime")]
        public DateTime StartTime { get; protected set; }
    }
}
