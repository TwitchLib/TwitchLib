using System.Text.Json.Serialization;

namespace TwitchLib.Api.ThirdParty.ModLookup
{
    public class ModLookupResponse
    {
        [JsonPropertyName("status")]
        public int Status { get; protected set; }
        [JsonPropertyName("user")]
        public string User { get; protected set; }
        [JsonPropertyName("count")]
        public int Count { get; protected set; }
        [JsonPropertyName("channels")]
        public ModLookupListing[] Channels { get; protected set; }
    }
}
