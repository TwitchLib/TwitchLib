using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Extensions.Transactions
{
    public class Transaction
    {
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; protected set; }
        [JsonPropertyName("broadcaster_id")]
        public string BroadcasterId { get; protected set; }
        [JsonPropertyName("broadcaster_login")]
        public string BroadcasterLogin { get; protected set; }
        [JsonPropertyName("broadcaster_name")]
        public string BroadcasterName { get; protected set; }
        [JsonPropertyName("user_id")]
        public string UserId { get; protected set; }
        [JsonPropertyName("user_name")]
        public string UserName { get; protected set; }
        [JsonPropertyName("product_type")]
        public string ProductType { get; protected set; }
        [JsonPropertyName("product_data")]
        public ProductData ProductData { get; protected set; }
    }
}
