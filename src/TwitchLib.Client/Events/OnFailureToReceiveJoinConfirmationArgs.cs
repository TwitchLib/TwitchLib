using System;
using TwitchLib.Client.Exceptions;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Class OnFailureToReceiveJoinConfirmationArgs.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class OnFailureToReceiveJoinConfirmationArgs : EventArgs
    {
        /// <summary>
        /// The exception
        /// </summary>
        public FailureToReceiveJoinConfirmationException Exception;
    }
}
