using System;

namespace TwitchLib.Client.Exceptions.Services
{
    /// <inheritdoc />
    /// <summary>Exception representing an invalid cache size provided.</summary>
    public class BadQueryCountException : Exception
    {
        /// <inheritdoc />
        /// <summary>Exception constructor.</summary>
        public BadQueryCountException(string data)
            : base(data)
        {
        }
    }
}
