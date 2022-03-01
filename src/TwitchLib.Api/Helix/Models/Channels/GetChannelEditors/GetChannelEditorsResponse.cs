using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Channels.GetChannelEditors
{
    public class GetChannelEditorsResponse
    {
        [JsonPropertyName("data")]
        public ChannelEditor[] Data { get; protected set; }
    }
}
