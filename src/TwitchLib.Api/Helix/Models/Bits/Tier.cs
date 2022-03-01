using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Bits
{
    public class Tier
    {
        [JsonPropertyName("min_bits")]
        public int MinBits { get; protected set; }
        [JsonPropertyName("id")]
        public string Id { get; protected set; }
        [JsonPropertyName("color")]
        public string Color { get; protected set; }
        [JsonPropertyName("images")]
        public string[] Images { get; protected set; }
        [JsonPropertyName("can_cheer")]
        public bool CanCheer { get; protected set; }
        [JsonPropertyName("show_in_bits_card")]
        public bool ShowInBitsCard { get; protected set; }
    }
}
