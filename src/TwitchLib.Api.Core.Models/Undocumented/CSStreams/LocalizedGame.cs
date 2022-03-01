using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.CSStreams
{
    public class LocalizedGame
    {
        [JsonPropertyName("name")]
        public string Name { get; protected set; }
        [JsonPropertyName("popularity")]
        public int Popularity { get; protected set; }
        [JsonPropertyName("_id")]
        public string Id { get; protected set; }
        [JsonPropertyName("giantbomb_id")]
        public string GiantbombId { get; protected set; }
        [JsonPropertyName("box")]
        public Box Box { get; protected set; }
        [JsonPropertyName("logo")]
        public Logo Logo { get; protected set; }
        [JsonPropertyName("localized_name")]
        public string LocalizedName { get; protected set; }
        [JsonPropertyName("locale")]
        public string Locale { get; protected set; }
    }
}
