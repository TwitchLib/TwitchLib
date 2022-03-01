using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Polls.EndPoll
{
    public class EndPollResponse
    {
        [JsonPropertyName("data")]
        public Poll[] Data { get; protected set; }
    }
}
