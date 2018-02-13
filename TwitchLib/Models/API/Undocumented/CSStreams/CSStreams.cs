using Newtonsoft.Json;

namespace TwitchLib.Models.API.Undocumented.CSStreams
{
    public class CSStreams
    {
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
        [JsonProperty(PropertyName = "streams")]
        public CSStream[] Streams { get; protected set; }
    }
}
