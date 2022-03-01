using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Polls.CreatePoll
{
    public class CreatePollRequest
    {
        [JsonPropertyName("broadcaster_id")]
        public string BroadcasterId { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("choices")]
        public Choice[] Choices { get; set; }
        [JsonPropertyName("bits_voting_enabled")]
        public bool BitsVotingEnabled { get; set; }
        [JsonPropertyName("bits_per_vote")]
        public int BitsPerVote { get; set; }
        [JsonPropertyName("channel_points_voting_enabled")]
        public bool ChannelPointsVotingEnabled { get; set; }
        [JsonPropertyName("channel_points_per_vote")]
        public int ChannelPointsPerVote { get; set; }
        [JsonPropertyName("duration")]
        public int DurationSeconds { get; set; }
    }
}
