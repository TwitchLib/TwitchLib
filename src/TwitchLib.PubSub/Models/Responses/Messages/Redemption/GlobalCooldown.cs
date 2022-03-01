using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.PubSub.Models.Responses.Messages.Redemption
{
    public class GlobalCooldown
    {
        [JsonPropertyName("is_enabled")]
        public string IsEnabled { get; protected set; }
        [JsonPropertyName("global_cooldown_seconds")]
        public int GlobalCooldownSeconds { get; protected set; }
    }
}
