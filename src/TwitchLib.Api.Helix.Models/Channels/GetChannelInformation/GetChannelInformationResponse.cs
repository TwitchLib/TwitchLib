using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Channels.GetChannelInformation
{
    public class GetChannelInformationResponse
    {
        [JsonPropertyName("data")]
        public ChannelInformation[] Data { get; protected set; }
    }
}
