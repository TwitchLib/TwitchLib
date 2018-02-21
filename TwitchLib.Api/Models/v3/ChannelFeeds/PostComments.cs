using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v3.ChannelFeeds
{
    public class PostComments
    {
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
        [JsonProperty(PropertyName = "_cursor")]
        public string Cursor { get; protected set; }
        [JsonProperty(PropertyName = "comments")]
        public Comment[] Comments { get; protected set; }
    }
}
