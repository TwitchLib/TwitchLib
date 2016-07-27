using System;

namespace TwitchLib.Exceptions
{
    /// <summary>Exception representing a bad channel.</summary>
    public class InvalidChannelException : Exception
    {
        /// <summary>Exception constructor</summary>
        public InvalidChannelException(string apiData)
            : base(apiData)
        {
        }
    }
}