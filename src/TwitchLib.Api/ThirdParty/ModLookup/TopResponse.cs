using System.Text.Json.Serialization;

namespace TwitchLib.Api.ThirdParty.ModLookup
{
    public class TopResponse
    {
        [JsonPropertyName("status")]
        public int Status { get; protected set; }
        [JsonPropertyName("top")]
        public Top Top { get; protected set; }
    }
}
