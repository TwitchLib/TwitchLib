using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.PubSub.Models.Responses.Messages.Redemption
{
    public class MaxPerStream
    {
        [JsonPropertyName("is_enabled")]
        public bool IsEnabled { get; protected set; }
        [JsonPropertyName("max_per_stream")]
        public int MaxPerStreamValue { get; protected set; }
    }
}
