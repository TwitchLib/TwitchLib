using System;

namespace TwitchLib.Exceptions.Client
{
    /// <inheritdoc />
    /// <summary>Exception thrown when attempting to assign a variable with a different value that is not allowed.</summary>
    public class ClientNotInitializedException : Exception
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public ClientNotInitializedException(string description)
            : base(description)
        {
        }
    }
}
