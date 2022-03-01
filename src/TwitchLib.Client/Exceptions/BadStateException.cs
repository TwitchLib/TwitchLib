using System;

namespace TwitchLib.Client.Exceptions
{
    /// <summary>
    /// Exception thrown when the state of the client cannot allow an operation to be run.
    /// Implements the <see cref="System.Exception" />
    /// </summary>
    /// <seealso cref="System.Exception" />
    /// <inheritdoc />
    public class BadStateException : Exception
    {
        /// <summary>
        /// Exception constructor
        /// </summary>
        /// <param name="details">The details.</param>
        /// <inheritdoc />
        public BadStateException(string details)
            : base(details)
        {
        }
    }
}
