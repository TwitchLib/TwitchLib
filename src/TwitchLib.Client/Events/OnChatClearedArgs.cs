using System;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Args representing a cleared chat event.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    /// <inheritdoc />
    public class OnChatClearedArgs : EventArgs
    {
        /// <summary>
        /// Channel that had chat cleared event.
        /// </summary>
        public string Channel;
    }
}
