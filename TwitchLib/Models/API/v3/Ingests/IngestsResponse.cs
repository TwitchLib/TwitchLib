using Newtonsoft.Json;

namespace TwitchLib.Models.API.v3.Ingests
{
    public class IngestsResponse
    {
        [JsonProperty(PropertyName = "ingests")]
        public Ingest[] Ingests { get; protected set; }
    }
}
