using System.Text.Json.Serialization;

namespace TwitchLib.Api.ThirdParty.ModLookup
{
    public class ModLookupListing
    {
        [JsonPropertyName("name")]
        public string Name { get; protected set; }
        [JsonPropertyName("followers")]
        public int Followers { get; protected set; }
        [JsonPropertyName("views")]
        public int Views { get; protected set; }
        [JsonPropertyName("partnered")]
        public bool Partnered { get; protected set; }
    }
}
