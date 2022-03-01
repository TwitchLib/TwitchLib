using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.PubSub.Models.Responses.Messages.Redemption
{
    public class MaxPerUserPerStream
    {
        [JsonPropertyName("is_enabled")]
        public string IsEnabled { get; protected set; }
        [JsonPropertyName("max_per_user_per_stream")]
        public int MaxPerUserPerStreamValue { get; protected set; }
    }
}
