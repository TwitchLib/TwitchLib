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
        public string Name { get; protected set; }
        public bool Default { get; protected set; }
        public string Id { get; protected set; }
        public string UrlTemplate { get; protected set; }
        public double Availability { get; protected set; }

        public Ingest(JToken json)
        {

        }
    }
}
