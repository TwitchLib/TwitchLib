using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Streams.CreateStreamMarker
{
    public class CreatedMarker
    {
        [JsonPropertyName("id")]
        public int Id { get; protected set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; protected set; }
        [JsonPropertyName("description")]
        public string Description { get; protected set; }
        [JsonPropertyName("position_seconds")]
        public int PositionSeconds { get; protected set; }
    }
}
