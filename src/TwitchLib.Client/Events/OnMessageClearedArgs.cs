using System;

namespace TwitchLib.Client.Events
{
    /// <summary>
    /// Args representing a cleared message event.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    /// <inheritdoc />
    public class OnMessageClearedArgs : EventArgs
    {
        /// <summary>
        /// Channel that had message cleared event.
        /// </summary>
        public string Channel;

        /// <summary>
        /// Message contents that received clear message
        /// </summary>
        public string Message;

        /// <summary>
        /// Message ID representing the message that was cleared
        /// </summary>
        public string TargetMessageId;

        /// <summary>
        /// Timestamp of when message was sent
        /// </summary>
        public string TmiSentTs;
    }
}
