using System;
using TwitchLib.Client.Models;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Args representing message sent event.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    /// <inheritdoc />
    public class OnMessageSentArgs : EventArgs
    {
        /// <summary>
        /// Property representing a chat message that was just sent (check null on properties before using).
        /// </summary>
        public SentMessage SentMessage;
    }
}
