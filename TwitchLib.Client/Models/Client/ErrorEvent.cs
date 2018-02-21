using System;

namespace TwitchLib.Client.Models.Client
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
