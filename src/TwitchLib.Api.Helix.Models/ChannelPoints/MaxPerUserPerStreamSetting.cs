using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.ChannelPoints
{
    public class MaxPerUserPerStreamSetting
    {
        [JsonPropertyName("is_enabled")]
        public bool IsEnabled { get; protected set; }
        [JsonPropertyName("max_per_user_per_stream")]
        public int MaxPerUserPerStream { get; protected set; }
    }
}
