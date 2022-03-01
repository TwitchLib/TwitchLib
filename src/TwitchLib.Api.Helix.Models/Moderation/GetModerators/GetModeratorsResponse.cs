using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Moderation.GetModerators
{
    public class GetModeratorsResponse
    {
        [JsonPropertyName("data")]
        public Moderator[] Data { get; protected set; }
        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
