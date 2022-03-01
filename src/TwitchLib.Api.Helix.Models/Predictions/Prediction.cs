using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Predictions
{
    public class Prediction
    {
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("broadcaster_id")]
        public string BroadcasterId { get; protected set; }
        [JsonPropertyName("broadcaster_name")]
        public string BroadcasterName { get; protected set; }
        [JsonPropertyName("broadcaster_login")]
        public string BroadcasterLogin { get; protected set; }
        [JsonPropertyName("title")]
        public string Title { get; protected set; }
        [JsonPropertyName("winning_outcome_id")]
        public string WinningOutcomeId { get; protected set; }
        [JsonPropertyName("outcomes")]
        public Outcome[] Outcomes { get; protected set; }
    }
}
