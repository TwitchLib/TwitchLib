using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Predictions.EndPrediction
{
    public class EndPredictionResponse
    {
        [JsonPropertyName("data")]
        public Prediction[] Data { get; protected set; }
    }
}
