using System.Text.Json.Serialization;

namespace TwitchLib.Api.Core.Models.Undocumented.Chatters
{
    public class Chatters
    {
        [JsonPropertyName("moderators")]
        public string[] Moderators { get; protected set; }
        [JsonPropertyName("staff")]
        public string[] Staff { get; protected set; }
        [JsonPropertyName("admins")]
        public string[] Admins { get; protected set; }
        [JsonPropertyName("global_mods")]
        public string[] GlobalMods { get; protected set; }
        [JsonPropertyName("vips")]
        public string[] VIP { get; protected set; }
        [JsonPropertyName("viewers")]
        public string[] Viewers { get; protected set; }
    }
}
