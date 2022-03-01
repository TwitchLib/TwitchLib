using System;
using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.TwitchPrimeOffers
{
    public class Offer
    {
        [JsonPropertyName("applicableGame")]
        public string ApplicableGame { get; protected set; }
        [JsonPropertyName("assets")]
        public Asset[] Assets { get; protected set; }
        [JsonPropertyName("contentCategories")]
        public string[] ContentCategories { get; protected set; }
        [JsonPropertyName("contentClaimInstructions")]
        public string ContentClaimInstruction { get; protected set; }
        [JsonPropertyName("contentDeliveryMethod")]
        public string ContentDeliveryMethod { get; protected set; }
        [JsonPropertyName("endTime")]
        public DateTime EndTime { get; protected set; }
        [JsonPropertyName("offerDescription")]
        public string OfferDescription { get; protected set; }
        [JsonPropertyName("offerId")]
        public string OfferId { get; protected set; }
        [JsonPropertyName("offerTitle")]
        public string OfferTitle { get; protected set; }
        [JsonPropertyName("priority")]
        public int Priority { get; protected set; }
        [JsonPropertyName("publisherName")]
        public string PublisherName { get; protected set; }
        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; protected set; }
    }
}
