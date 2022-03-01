using System.Text.Json.Serialization;
using TwitchLib.Api.Helix.Models.Users.Internal;

namespace TwitchLib.Api.Helix.Models.Users.GetUserActiveExtensions
{
    public class GetUserActiveExtensionsResponse
    {
        [JsonPropertyName("data")]
        public ActiveExtensions Data { get; protected set; }
    }
}
