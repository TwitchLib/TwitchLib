using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.Client
{
    /// <summary>Class representing a detection return object.</summary>
    public class DetectionReturn
    {
        /// <summary>Property representing whether detection was successful.</summary>
        public bool Successful { get; protected set; }
        /// <summary>Property representing the detected channel, could be null.</summary>
        public string Channel { get; protected set; }

        /// <summary>DetectionReturn object constructor.</summary>
        public DetectionReturn(bool successful, string channel = null)
        {
            Successful = successful;
            Channel = channel;
        }
    }
}
