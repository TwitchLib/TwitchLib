using Newtonsoft.Json;
using System;

namespace TwitchLib.Api.Models.v3.Streams
{
    public class Stream
    {
        [JsonProperty(PropertyName = "game")]
        public string Game { get; protected set; }
        [JsonProperty(PropertyName = "viewers")]
        public int Viewers { get; protected set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; protected set; }
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "channel")]
        public Channels.Channel Channel { get; protected set; }
        [JsonProperty(PropertyName = "preview")]
        public Preview Preview { get; protected set; }
    }
}
