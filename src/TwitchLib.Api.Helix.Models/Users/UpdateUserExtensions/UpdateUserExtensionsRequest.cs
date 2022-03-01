using System.Text.Json.Serialization;
using System.Collections.Generic;
using TwitchLib.Api.Helix.Models.Users.Internal;

namespace TwitchLib.Api.Helix.Models.Users.UpdateUserExtensions
{
    public class UpdateUserExtensionsRequest
    {
        [JsonPropertyName("data")]
        public UpdateUserExtensionsRequestData Data { get; set; }
    }
}
