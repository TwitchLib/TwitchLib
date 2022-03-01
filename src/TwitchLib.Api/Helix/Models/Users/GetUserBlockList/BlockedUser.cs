using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Users.GetUserBlockList
{
    public class BlockedUser
    {
        [JsonPropertyName("user_id")]
        public string Id { get; protected set; }
        [JsonPropertyName("user_login")]
        public string UserLogin { get; protected set; }
        [JsonPropertyName("display_name")]
        public string DisplayName { get; protected set; }
    }
}
