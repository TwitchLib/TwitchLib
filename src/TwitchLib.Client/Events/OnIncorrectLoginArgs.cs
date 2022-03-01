using System;
using TwitchLib.Client.Exceptions;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Args representing an incorrect login event.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    /// <inheritdoc />
    public class OnIncorrectLoginArgs : EventArgs
    {
        /// <summary>
        /// Property representing exception object.
        /// </summary>
        public ErrorLoggingInException Exception;
    }
}
