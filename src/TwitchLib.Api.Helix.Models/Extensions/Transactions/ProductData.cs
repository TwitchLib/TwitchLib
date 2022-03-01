using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Extensions.Transactions
{
    public class ProductData
    {
        [JsonPropertyName("sku")]
        public string SKU { get; protected set; }
        [JsonPropertyName("cost")]
        public Cost Cost { get; protected set; }
        [JsonPropertyName("displayName")]
        public string DisplayName { get; protected set; }
        [JsonPropertyName("inDevelopment")]
        public bool InDevelopment { get; protected set; }
    }

    public class Cost
    {
        [JsonPropertyName("amount")]
        public int Amount { get; protected set; }
        [JsonPropertyName("type")]
        public string Type { get; protected set; }
    }
}
