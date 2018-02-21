using Newtonsoft.Json;

namespace TwitchLib.Api.Models.Undocumented.RecentEvents
{
    public class Top
    {
        [JsonProperty(PropertyName = "has_top_event")]
        public bool HasTopEvent { get; protected set; }
        [JsonProperty(PropertyName = "message_id")]
        public string MessageId { get; protected set; }
        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; protected set; }
        [JsonProperty(PropertyName = "bits_used")]
        public int? BitsUsed { get; protected set; }
        [JsonProperty(PropertyName = "message")]
        public string Message { get; protected set; }
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; protected set; }
        [JsonProperty(PropertyName = "username")]
        public string Username { get; protected set; }
        //TODO: consider tags param
    }
}
