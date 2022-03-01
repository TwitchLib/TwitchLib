using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.RecentEvents
{
    public class Top
    {
        [JsonPropertyName("has_top_event")]
        public bool HasTopEvent { get; protected set; }
        [JsonPropertyName("message_id")]
        public string MessageId { get; protected set; }
        [JsonPropertyName("amount")]
        public int Amount { get; protected set; }
        [JsonPropertyName("bits_used")]
        public int? BitsUsed { get; protected set; }
        [JsonPropertyName("message")]
        public string Message { get; protected set; }
        [JsonPropertyName("user_id")]
        public string UserId { get; protected set; }
        [JsonPropertyName("username")]
        public string Username { get; protected set; }
        //TODO: consider tags param
    }
}
