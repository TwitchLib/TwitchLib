using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Bits
{
    public class GetBitsLeaderboardResponse
    {
        [JsonPropertyName("data")]
        public Listing[] Listings { get; protected set; }
        [JsonPropertyName("date_range")]
        public DateRange DateRange { get; protected set; }
        [JsonPropertyName("total")]
        public int Total { get; protected set; }
    }
}
