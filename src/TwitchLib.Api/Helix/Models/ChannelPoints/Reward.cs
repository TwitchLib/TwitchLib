using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.ChannelPoints
{
    public class Reward
    {
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("title")]
        public string Title { get; protected set; }
        [JsonPropertyName("prompt")]
        public string Prompt { get; protected set; }
        [JsonPropertyName("cost")]
        public int Cost { get; protected set; }
    }
}
