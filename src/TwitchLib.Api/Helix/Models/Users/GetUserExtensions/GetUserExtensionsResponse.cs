using System.Text.Json.Serialization;
using TwitchLib.Api.Helix.Models.Users.Internal;

namespace TwitchLib.Api.Helix.Models.Users.GetUserExtensions
{
    public class GetUserExtensionsResponse
    {
        [JsonPropertyName("data")]
        public UserExtension[] Users { get; protected set; }
    }
}
