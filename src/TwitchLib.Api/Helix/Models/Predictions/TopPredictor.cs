using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Predictions
{
    public class TopPredictor
    {
        [JsonPropertyName("user")]
        public User User { get; protected set; }
    }
}
