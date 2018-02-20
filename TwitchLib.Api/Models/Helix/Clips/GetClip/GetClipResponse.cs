using Newtonsoft.Json;

namespace TwitchLib.Api.Models.Helix.Clips.GetClip
{
    public class GetClipResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Clip[] Clips { get; protected set; }
    }
}
