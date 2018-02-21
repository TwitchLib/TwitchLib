using Newtonsoft.Json;

namespace TwitchLib.Api.Models.Undocumented.CSStreams
{
    public class CSStream
    {
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "game")]
        public string Game { get; protected set; }
        [JsonProperty(PropertyName = "viewers")]
        public int Viewers { get; protected set; }
        [JsonProperty(PropertyName = "map")]
        public string Map { get; protected set; }
        [JsonProperty(PropertyName = "map_name")]
        public string MapName { get; protected set; }
        [JsonProperty(PropertyName = "map_img")]
        public string MapImg { get; protected set; }
        [JsonProperty(PropertyName = "skill")]
        public int Skill { get; protected set; }
        [JsonProperty(PropertyName = "preview")]
        public Preview Preview { get; protected set; }
        [JsonProperty(PropertyName = "is_playlist")]
        public bool IsPlaylist { get; protected set; }
        [JsonProperty(PropertyName = "user")]
        public User User { get; protected set; }
    }
}
