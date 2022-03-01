using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Moderation.CheckAutoModStatus
{
    public class Message
    {
        [JsonPropertyName("msg_id")]
        public string MsgId { get; set; }
        [JsonPropertyName("msg_text")]
        public string MsgText { get; set; }
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }
    }
}
