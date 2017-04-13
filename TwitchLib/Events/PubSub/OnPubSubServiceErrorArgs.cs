using System;

namespace TwitchLib.Events.PubSub
{
    /// <summary>Class representing a pubsub service error event.</summary>
    public class OnPubSubServiceErrorArgs
    {
        /// <summary>Property representing exception.</summary>
        public Exception Exception;
    }
}
