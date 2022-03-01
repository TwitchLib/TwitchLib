using System.Text.Json.Serialization;
using TwitchLib.Api.Helix.Models.Common;
using TwitchLib.Api.Helix.Models.Games;

namespace TwitchLib.Api.Helix.Models.Search
{
    public class SearchCategoriesResponse
    {
        [JsonPropertyName("data")]
        public Game[] Games { get; protected set; }
        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
