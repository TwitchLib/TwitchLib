using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace TwitchLib.Models.API.Community
{
    /// <summary>Model representing the streams in a current community.</summary>
    public class StreamsInCommunityResponse
    {
        /// <summary>Total number </summary>
        public int Total { get; protected set; }
        /// <summary>List of streams that are currently in the community.</summary>
        public List<Stream.Stream> Streams { get; protected set; } = new List<Stream.Stream>();

        /// <summary>StreamsInCommunityResponse constructor.</summary>
        /// <param name="json"></param>
        public StreamsInCommunityResponse(JToken json)
        {
            if (json.SelectToken("_total") != null)
                Total = int.Parse(json.SelectToken("_total").ToString());
            if (json.SelectToken("streams") != null)
                foreach (JToken stream in json.SelectToken("streams"))
                    Streams.Add(new Stream.Stream(stream));
        }
    }
}
