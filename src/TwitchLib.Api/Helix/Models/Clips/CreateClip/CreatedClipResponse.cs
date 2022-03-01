using System.Text.Json.Serialization;

namespace TwitchLib.Api.Helix.Models.Clips.CreateClip
{
    public class CreatedClipResponse
    {
        [JsonPropertyName("data")]
        public CreatedClip[] CreatedClips { get; protected set; }
    }
}
