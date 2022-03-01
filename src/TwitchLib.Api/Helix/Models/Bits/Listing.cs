using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Bits
{
    public class Listing
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; protected set; }
        [JsonPropertyName("user_login")]
        public string UserLogin { get; protected set; }
        [JsonPropertyName("user_name")]
        public string UserName { get; protected set; }
        [JsonPropertyName("rank")]
        public int Rank { get; protected set; }
        [JsonPropertyName("score")]
        public int Score { get; protected set; }
    }
}
