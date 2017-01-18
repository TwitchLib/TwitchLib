using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Video
{
    /// <summary>Class representing FPS data.</summary>
    public class FPS
    {
        /// <summary>FPS can return data either in "high", or in "1080p". IF this boolean is set, it means that the integer values (1080p, 144p, etc) properties are being used.</summary>
        public bool UsingIntegerQualities = false;
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

        /// <summary></summary>
        public double Chunked { get; protected set; }
        /// <summary></summary>
        public double High { get; protected set; }
        /// <summary></summary>
        public double Low { get; protected set; }
        /// <summary></summary>
        public double Medium { get; protected set; }
        /// <summary></summary>
        public double Mobile { get; protected set; }

        /// <summary>
        /// FPS data constructor
        /// </summary>
        /// <param name="json"></param>
        public FPS(JToken json)
        {
            if(json.SelectToken("chunked") != null)
            {
                UsingIntegerQualities = false;
                Chunked = double.Parse(json.SelectToken("chunked").ToString());
                High = double.Parse(json.SelectToken("high").ToString());
                Low = double.Parse(json.SelectToken("low").ToString());
                Medium = double.Parse(json.SelectToken("medium").ToString());
                Mobile = double.Parse(json.SelectToken("mobile").ToString());
            } else
            {
                UsingIntegerQualities = true;
                p1080 = json.SelectToken("1080p")?.ToString();
                p144 = json.SelectToken("144p")?.ToString();
                p240 = json.SelectToken("240p")?.ToString();
                p360 = json.SelectToken("360p")?.ToString();
                p480 = json.SelectToken("480p")?.ToString();
                p720 = json.SelectToken("720p")?.ToString();
            }
            
        }
    }
}
