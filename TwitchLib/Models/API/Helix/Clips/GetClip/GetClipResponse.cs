using Newtonsoft.Json;

namespace TwitchLib.Models.API.Helix.Clips.GetClip
{
    public class GetClipResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Clip[] Clips { get; protected set; }
    }
}
