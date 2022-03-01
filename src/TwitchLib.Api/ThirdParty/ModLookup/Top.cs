using System.Text.Json.Serialization;

namespace TwitchLib.Api.ThirdParty.ModLookup
{
    public class Top
    {
        [JsonPropertyName("modcount")]
        public ModLookupListing[] ModCount { get; protected set; }
        [JsonPropertyName("views")]
        public ModLookupListing[] Views { get; protected set; }
        [JsonPropertyName("followers")]
        public ModLookupListing[] Followers { get; protected set; }
    }
}
