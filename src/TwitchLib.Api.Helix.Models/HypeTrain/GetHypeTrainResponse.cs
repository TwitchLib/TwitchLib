using System.Text.Json.Serialization;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.HypeTrain
{
    public class GetHypeTrainResponse
    {
        [JsonPropertyName("data")]
        public HypeTrain[] HypeTrain { get; protected set; }
        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; protected set; }
    }
}