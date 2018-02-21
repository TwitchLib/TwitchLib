using System;

namespace TwitchLib.Client.Exceptions.Client
{
    /// <inheritdoc />
    /// <summary>Exception thrown when the state of the client cannot allow an operation to be run.</summary>
    public class BadStateException : Exception
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public BadStateException(string details)
            : base(details)
        {
        }
    }
}
