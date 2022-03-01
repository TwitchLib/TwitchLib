using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Common
{
    public class Tag
    {
        [JsonPropertyName("tag_id")]
        public string TagId { get; protected set; }
        [JsonPropertyName("is_auto")]
        public bool IsAuto { get; protected set; }
        [JsonPropertyName("localization_names")]
        public Dictionary<string, string> LocalizationNames { get; protected set; }
        [JsonPropertyName("localization_descriptions")]
        public Dictionary<string, string> LocalizationDescriptions { get; protected set; }
    }
}
