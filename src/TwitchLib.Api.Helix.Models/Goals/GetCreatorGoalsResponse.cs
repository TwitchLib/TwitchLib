using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Goals
{
    public class GetCreatorGoalsResponse
    {
        [JsonPropertyName("data")]
        public CreatorGoal[] Data { get; protected set; }
    }
}
