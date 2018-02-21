using Newtonsoft.Json;

namespace TwitchLib.Api.Models.Helix.Clips.CreateClip
{
    public class CreatedClipResponse
    {
        [JsonProperty(PropertyName = "data")]
        public CreatedClip[] CreatedClips { get; protected set; }
    }
}
