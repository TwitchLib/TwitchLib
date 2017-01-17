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
        /// <summary>Property representing FPS for audio only.</summary>
        public double AudioOnly { get; protected set; }
        /// <summary>Property representing FPS for medium quality.</summary>
        public double Medium { get; protected set; }
        /// <summary>Property representing FPS for mobile quality.</summary>
        public double Mobile { get; protected set; }
        /// <summary>Property representing FPS for high quality.</summary>
        public double High { get; protected set; }
        /// <summary>Property representing FPS for low quality.</summary>
        public double Low { get; protected set; }
        /// <summary>Property representing FPS for chunked quality.</summary>
        public double Chunked { get; protected set; }

        /// <summary>
        /// FPS Data constructor.
        /// </summary>
        /// <param name="audioOnly"></param>
        /// <param name="medium"></param>
        /// <param name="mobile"></param>
        /// <param name="high"></param>
        /// <param name="low"></param>
        /// <param name="chunked"></param>
        public FPS(double audioOnly, double medium, double mobile, double high, double low, double chunked)
        {
            AudioOnly = audioOnly;
            Low = low;
            Medium = medium;
            Mobile = mobile;
            High = high;
            Chunked = chunked;
        }

        /// <summary>Returns string in format: audio only: {}, mobile: {} etc.</summary>
        public override string ToString()
        {
            return
                $"audio only: {AudioOnly}, mobile: {Mobile}, low: {Low}, medium: {Medium}, high: {High}, chunked: {Chunked}";
        }
    }
}
