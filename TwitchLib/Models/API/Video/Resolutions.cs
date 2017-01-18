using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Video
{
    /// <summary>Class representing resolution data.</summary>
    public class Resolutions
    {
        /// <summary></summary>
        public string p1080 { get; protected set; }
        /// <summary></summary>
        public string p144 { get; protected set; }
        /// <summary></summary>
        public string p240 { get; protected set; }
        /// <summary></summary>
        public string p360 { get; protected set; }
        /// <summary></summary>
        public string p480 { get; protected set; }
        /// <summary></summary>
        public string p720 { get; protected set; }

        /// <summary>
        /// Resolutions data constructor
        /// </summary>
        /// <param name="json"></param>
        public Resolutions(JToken json)
        {
            p1080 = json.SelectToken("1080p")?.ToString();
            p144 = json.SelectToken("144p")?.ToString();
            p240 = json.SelectToken("240p")?.ToString();
            p360 = json.SelectToken("360p")?.ToString();
            p480 = json.SelectToken("480p")?.ToString();
            p720 = json.SelectToken("720p")?.ToString();
        }
    }
}
