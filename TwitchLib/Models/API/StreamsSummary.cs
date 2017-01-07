using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TwitchLib.Models.API
{
    /// <summary>Class representing the response from Twtich regarding streams summary.</summary>
    public class StreamsSummary
    {
        /// <summary>Total number of viewers across all streams on Twitch.</summary>
        public int TotalViewers { get; protected set; }
        /// <summary>Total number of streams across all of Twitch.</summary>
        public int TotalStreams { get; protected set; }

        /// <summary>StreamSummary constructor.</summary>
        /// <param name="jsonStr"></param>
        public StreamsSummary(string jsonStr)
        {
            JObject json = JObject.Parse(jsonStr);
            TotalViewers = int.Parse(json.SelectToken("viewers").ToString());
            TotalStreams = int.Parse(json.SelectToken("channels").ToString());
        }
    }
}
