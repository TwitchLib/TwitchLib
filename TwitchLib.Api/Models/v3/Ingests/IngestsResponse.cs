using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v3.Ingests
{
    public class IngestsResponse
    {
        [JsonProperty(PropertyName = "ingests")]
        public Ingest[] Ingests { get; protected set; }
    }
}
