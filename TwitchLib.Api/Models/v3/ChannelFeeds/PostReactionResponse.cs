using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v3.ChannelFeeds
{
    public class PostReactionResponse
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; protected set; }
        [JsonProperty(PropertyName = "emote_id")]
        public string EmoteId { get; protected set; }
        [JsonProperty(PropertyName = "user")]
        public Users.User User { get; protected set; }
    }
}
