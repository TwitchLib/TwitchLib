using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Predictions.CreatePrediction
{
    public class CreatePredictionRequest
    {
        [JsonPropertyName("broadcaster_id")]
        public string BroadcasterId { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("outcomes")]
        public Outcome[] Outcomes { get; set; }
        [JsonPropertyName("prediction_window")]
        public int PredictionWindowSeconds { get; set; }
    }
}
