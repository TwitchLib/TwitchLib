using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Predictions
{
    public class User
    {
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("name")]
        public string Name { get; protected set; }
        [JsonPropertyName("login")]
        public string Login { get; protected set; }
        [JsonPropertyName("channel_points_used")]
        public int ChannelPointsUsed { get; protected set; }
        [JsonPropertyName("channel_points_won")]
        public int ChannelPointsWon { get; protected set; }
    }
}
