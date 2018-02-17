using Newtonsoft.Json;

namespace TwitchLib.Models.API.Helix.Clips.CreateClip
{
    public class CreatedClipResponse
    {
        [JsonProperty(PropertyName = "data")]
        public CreatedClip[] CreatedClips { get; protected set; }
    }
}
