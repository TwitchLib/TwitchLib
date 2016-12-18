using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Enums
{
    /// <summary>
    /// Enum to show which direction the message was detected from.
    /// </summary>
    public enum SendReceiveDirection
    {
        /// <summary>Used for logging, direction of data.</summary>
        Sent,
        /// <summary>Used for logging, direction of data.</summary>
        Received
    }
}
