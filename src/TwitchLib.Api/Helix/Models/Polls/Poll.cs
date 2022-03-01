using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Polls
{
    public class Poll
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
        [JsonPropertyName("choices")]
        public Choice[] Choices { get; protected set; }
        [JsonPropertyName("bits_voting_enabled")]
        public bool BitsVotingEnabled { get; protected set; }
        [JsonPropertyName("bits_per_vote")]
        public int BitsPerVote { get; protected set; }
        [JsonPropertyName("channel_points_voting_enabled")]
        public bool ChannelPointsVotingEnabled { get; protected set; }
        [JsonPropertyName("channel_points_per_vote")]
        public int ChannelPointsPerVote { get; protected set; }
        [JsonPropertyName("status")]
        public string Status { get; protected set; }
        [JsonPropertyName("duration")]
        public int DurationSeconds { get; protected set; }
        [JsonPropertyName("started_at")]
        public DateTime StartedAt { get; protected set; }
    }
}
