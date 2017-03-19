using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Streams
{
    public class StreamsResponse
    {
        public int Total { get; protected set; }
        public List<Stream> Streams { get; protected set; } = new List<Stream>();

        public StreamsResponse(JToken json)
        {

        }
    }
}
