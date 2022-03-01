using Newtonsoft.Json;
using TwitchLib.Api.Core.Interfaces.Clips;

namespace TwitchLib.Api.V5.Models.Clips
{
    public class VOD : IVOD
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; protected set; }
        [JsonProperty(PropertyName = "offset")]
        public int Offset { get; protected set; }
        [JsonProperty(PropertyName = "preview_image_url")]
        public string PreviewImageUrl { get; protected set; }
    }
}
