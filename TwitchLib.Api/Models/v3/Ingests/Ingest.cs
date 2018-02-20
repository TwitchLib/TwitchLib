using Newtonsoft.Json;

namespace TwitchLib.Api.Models.v3.Ingests
{
    public class Ingest
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }
        [JsonProperty(PropertyName = "default")]
        public bool Default { get; protected set; }
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "url_template")]
        public string UrlTemplate { get; protected set; }
        [JsonProperty(PropertyName = "availability")]
        public double Availability { get; protected set; }
    }
}
