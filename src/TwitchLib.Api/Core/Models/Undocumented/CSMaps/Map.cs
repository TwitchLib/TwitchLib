using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.CSMaps
{
    public class Map
    {
        [JsonPropertyName("map")]
        public string MapCode { get; protected set; }
        [JsonPropertyName("map_name")]
        public string MapName { get; protected set; }
        [JsonPropertyName("map_image")]
        public string MapImage { get; protected set; }
        [JsonPropertyName("viewers")]
        public int Viewers { get; protected set; }
    }
}
