using Newtonsoft.Json;

namespace TwitchLib.Api.Models.Undocumented.ChannelPanels
{
    public class Data
    {
        [JsonProperty(PropertyName = "link")]
        public string Link { get; protected set; }
        [JsonProperty(PropertyName = "image")]
        public string Image { get; protected set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }
    }
}
