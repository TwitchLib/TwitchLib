using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Undocumented.RecentEvents
{
    public class RecentEvents
    {
        [JsonProperty(PropertyName = "recent")]
        public Recent Recent { get; protected set; }
        [JsonProperty(PropertyName = "top")]
        public Top Top { get; protected set; }
        [JsonProperty(PropertyName = "has_recent_event")]
        public bool HasRecentEvent { get; protected set; }
        [JsonProperty(PropertyName = "message_id")]
        public string MessageId { get; protected set; }
        [JsonProperty(PropertyName = "timestamp")]
        public string Timestamp { get; protected set; }
        [JsonProperty(PropertyName = "channel_id")]
        public string ChannelId { get; protected set; }
        [JsonProperty(PropertyName = "allotted_time_ms")]
        public string AllottedTimeMs { get; protected set; }
        [JsonProperty(PropertyName = "time_remaining_ms")]
        public string TimeRemainingMs { get; protected set; }
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
        // TODO: consider tags property
    }
}
