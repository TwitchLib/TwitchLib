using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreamKey
{
    public class StreamKey
    {
        [JsonPropertyName("stream_key")]
        public string Key { get; protected set; }
    }
}
