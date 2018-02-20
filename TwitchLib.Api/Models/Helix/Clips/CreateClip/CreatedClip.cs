using Newtonsoft.Json;

namespace TwitchLib.Api.Models.Helix.Clips.CreateClip
{
    public class CreatedClip
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "edit_url")]
        public string EditUrl { get; protected set; }
    }
}
