using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.API.Video
{
    /// <summary>Class representing resolution data.</summary>
    public class Resolution
    {
        /// <summary>Property representing relation for medium quality.</summary>
        public string Medium { get; protected set; }
        /// <summary>Property representing relation for mobile quality.</summary>
        public string Mobile { get; protected set; }
        /// <summary>Property representing relation for high quality.</summary>
        public string High { get; protected set; }
        /// <summary>Property representing relation for low quality.</summary>
        public string Low { get; protected set; }
        /// <summary>Property representing relation for chunked quality.</summary>
        public string Chunked { get; protected set; }

        /// <summary>
        /// Resolutions data constructor
        /// </summary>
        /// <param name="medium"></param>
        /// <param name="mobile"></param>
        /// <param name="high"></param>
        /// <param name="low"></param>
        /// <param name="chunked"></param>
        public Resolution(string medium, string mobile, string high, string low, string chunked)
        {
            Medium = medium;
            Mobile = mobile;
            High = high;
            Low = low;
            Chunked = chunked;
        }

        /// <summary>Returns string in format: mobile: {}, low: {} etc</summary>
        public override string ToString()
        {
            return $"mobile: {Mobile}, low: {Low}, medium: {Medium}, high: {High}, chunked: {Chunked}";
        }
    }
}
