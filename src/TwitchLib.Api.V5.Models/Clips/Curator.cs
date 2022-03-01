using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Clips
{
    public class Curator
    {
        [JsonProperty(PropertyName = "channel_url")]
        public string ChannelUrl { get; protected set; }
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; protected set; }
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "logo")]
        public string Logo { get; protected set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }
    }
}
