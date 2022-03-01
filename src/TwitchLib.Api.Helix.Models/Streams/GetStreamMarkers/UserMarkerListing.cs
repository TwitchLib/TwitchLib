using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreamMarkers
{
    public class UserMarkerListing
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; protected set; }
        [JsonPropertyName("username")]
        public string UserName { get; protected set; }
        [JsonPropertyName("user_login")]
        public string UserLogin { get; protected set; }
        [JsonPropertyName("videos")]
        public Video[] Videos { get; protected set; }
    }
}
