using System;
using TwitchLib.Client.Exceptions.Client;

namespace TwitchLib.Client.Events.Client
{
    /// <inheritdoc />
    /// <summary>Args representing an incorrect login event.</summary>
    public class OnIncorrectLoginArgs : EventArgs
    {
        /// <summary>Property representing exception object.</summary>
        public ErrorLoggingInException Exception;
    }
}
