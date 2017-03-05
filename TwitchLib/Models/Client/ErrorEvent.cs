using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Models.Client
{
    /// <summary>Class representing the error that the websocket encountered.</summary>
    public class ErrorEvent
    {
        /// <summary>Message pertaining to the error.</summary>
        public string Message { get; internal set; }
        /// <summary>Exception object representing the error.</summary>
        public Exception Exception { get; internal set; }
    }
}
