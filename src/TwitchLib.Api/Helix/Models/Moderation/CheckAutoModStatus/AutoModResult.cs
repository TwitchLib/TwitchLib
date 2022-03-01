using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Moderation.CheckAutoModStatus
{
    public class AutoModResult
    {
        [JsonPropertyName("msg_id")]
        public string MsgId { get; protected set; }
        [JsonPropertyName("is_permitted")]
        public bool IsPermitted { get; protected set; }
    }
}
