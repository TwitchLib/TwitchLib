using System;
using TwitchLib.Exceptions.Client;

namespace TwitchLib.Events.Client
{
    /// <inheritdoc />
    /// <summary>Args representing an incorrect login event.</summary>
    public class OnIncorrectLoginArgs : EventArgs
    {
        /// <summary>Property representing exception object.</summary>
        public ErrorLoggingInException Exception;
    }
}
