using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Ingests
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
