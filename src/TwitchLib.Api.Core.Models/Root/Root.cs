using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Root
{
    public class Root
    {
        /// <summary>Property representing token object.</summary>
        [JsonPropertyName("token")]
        public RootToken Token { get; protected set; }
    }
}
