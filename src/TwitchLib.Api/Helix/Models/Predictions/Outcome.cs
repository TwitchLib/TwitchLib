using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Predictions
{
    public class Outcome
    {
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("title")]
        public string Title { get; protected set; }
        [JsonPropertyName("users")]
        public int ChannelPoints { get; protected set; }
        [JsonPropertyName("channel_points")]
        public int ChannelPointsVotes { get; protected set; }
        [JsonPropertyName("top_predictors")]
        public TopPredictor[] TopPredictors { get; protected set; }
        [JsonPropertyName("color")]
        public string Color { get; protected set; }
    }
}
