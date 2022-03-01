using System;

namespace TwitchLib.PubSub.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Class representing a pubsub service error event.
    /// </summary>
    public class OnPubSubServiceErrorArgs : EventArgs
    {
        /// <summary>
        /// Property representing exception.
        /// </summary>
        public Exception Exception;
    }
}
