using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Videos.DeleteVideos
{
    public class DeleteVideosResponse
    {
        [JsonPropertyName("data")]
        public string[] Data { get; protected set; }
    }
}
