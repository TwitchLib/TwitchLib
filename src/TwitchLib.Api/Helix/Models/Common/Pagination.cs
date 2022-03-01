using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Common
{
    public class Pagination
    {
        [JsonPropertyName("cursor")]
        public string Cursor { get; protected set; }
    }
}
