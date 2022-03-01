using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Streams.CreateStreamMarker
{
    public class CreateStreamMarkerResponse
    {
        [JsonPropertyName("data")]
        public CreatedMarker[] Data { get; protected set; }
    }
}
