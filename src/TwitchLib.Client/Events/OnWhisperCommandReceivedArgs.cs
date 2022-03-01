using System;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Args representing whisper command received event.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    /// <inheritdoc />
    public class OnWhisperCommandReceivedArgs : EventArgs
    {
        /// <summary>
        /// Property representing received command.
        /// </summary>
        public WhisperCommand Command;
    }
}
