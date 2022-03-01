using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Bits
{
    public class Cheermote
    {
        [JsonPropertyName("prefix")]
        public string Prefix { get; protected set; }
        [JsonPropertyName("tiers")]
        public Tier[] Tiers { get; protected set; }
        [JsonPropertyName("type")]
        public string Type { get; protected set; }
        [JsonPropertyName("order")]
        public int Order { get; protected set; }
        [JsonPropertyName("last_updated")]
        public DateTime LastUpdated { get; protected set; }
        [JsonPropertyName("is_charitable")]
        public bool IsCharitable { get; protected set; }
    }
}
