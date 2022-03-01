using System.Text.Json.Serialization;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Extensions.Transactions
{
    public class GetExtensionTransactionsResponse
    {
        [JsonPropertyName("data")]
        public Transaction[] Data { get; protected set; }
        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
