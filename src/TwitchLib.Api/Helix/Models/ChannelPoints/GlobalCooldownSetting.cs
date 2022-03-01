using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.ChannelPoints
{
    public class GlobalCooldownSetting
    {
        [JsonPropertyName("is_enabled")]
        public bool IsEnabled { get; protected set; }
        [JsonPropertyName("global_cooldown_seconds")]
        public int GlobalCooldownSeconds { get; protected set; }
    }
}
