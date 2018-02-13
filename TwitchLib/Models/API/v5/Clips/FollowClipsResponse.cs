using Newtonsoft.Json;

namespace TwitchLib.Models.API.v5.Clips
{
    public class FollowClipsResponse
    {
        [JsonProperty(PropertyName = "_cursor")]
        public string Cursor { get; protected set; }
        [JsonProperty(PropertyName = "clips")]
        public Clip[] Clips { get; protected set; }
    }
}
