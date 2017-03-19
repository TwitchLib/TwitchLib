using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.v3.Search
{
    public class SearchStreamsResponse
    {
        public int Total { get; protected set; }
        public List<Streams.Stream> Streams { get; protected set; } = new List<v3.Streams.Stream>();

        public SearchStreamsResponse(JToken json)
        {

        }
    }
}
