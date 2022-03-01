using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Polls
{
    public class Choice
    {
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("title")]
        public string Title { get; protected set; }
        [JsonPropertyName("votes")]
        public int Votes { get; protected set; }
        [JsonPropertyName("channel_points_votes")]
        public int ChannelPointsVotes { get; protected set; }
        [JsonPropertyName("bits_votes")]
        public int BitsVotes { get; protected set; }
    }
}
