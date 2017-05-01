namespace TwitchLib.Models.Client
{
    #region using directives
    using System;
    #endregion
    /// <summary>Class representing the error that the websocket encountered.</summary>
    public class ErrorEvent
    {
        /// <summary>Message pertaining to the error.</summary>
        public string Message { get; internal set; }
        /// <summary>Exception object representing the error.</summary>
        public Exception Exception { get; internal set; }
    }
}
