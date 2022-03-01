using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.ChannelPoints
{
    public class DefaultImage
    {

        [JsonPropertyName("url_1x")]
        public string Url1x { get; }
        [JsonPropertyName("url_2x")]
        public string Url2x { get; }
        [JsonPropertyName("url_4x")]
        public string Url4x { get; }
    }
}
