using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Stream
{
    /// <summary>Object representing followed streams response.</summary>
    public class FollowedStreamsResponse
    {
        /// <summary>Total number of followed streams (may be larger than 100).</summary>
        public int Total { get; protected set; }
        /// <summary>List of followed streams (up to 100)</summary>
        public List<Stream> Streams { get; protected set; } = new List<Stream>();

        /// <summary>FollowedStreamsResponse constructor.</summary>
        /// <param name="json"></param>
        public FollowedStreamsResponse(JToken json)
        {
            if (json.SelectToken("_total") != null)
                Total = int.Parse(json.SelectToken("_total").ToString());

            if (json.SelectToken("_streams") != null)
                foreach (JToken stream in json.SelectToken("_streams"))
                    Streams.Add(new Stream(stream));
        }
    }
}
