using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v3.Games
{
    public class TopGame
    {
        [JsonProperty(PropertyName = "game")]
        public Game Game { get; protected set; }
        [JsonProperty(PropertyName = "viewers")]
        public int Viewers { get; protected set; }
        [JsonProperty(PropertyName = "channels")]
        public int Channels { get; protected set; }
    }
}
