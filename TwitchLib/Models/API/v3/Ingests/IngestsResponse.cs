using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Ingests
{
    public class IngestsResponse
    {
        [JsonProperty(PropertyName = "ingests")]
        public Ingest[] Ingests { get; protected set; }
    }
}
