using System;

namespace TwitchLib.Client.Exceptions
{
    /// <summary>
    /// Exception thrown when attempting to perform an actino that is only available when the client is connected.
    /// Implements the <see cref="System.Exception" />
    /// </summary>
    /// <seealso cref="System.Exception" />
    /// <inheritdoc />
    public class ClientNotConnectedException : Exception
    {
        /// <summary>
        /// Exception constructor
        /// </summary>
        /// <param name="description">The description.</param>
        /// <inheritdoc />
        public ClientNotConnectedException(string description)
            : base(description)
        {
        }
    }
}
