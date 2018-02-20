using System;

namespace TwitchLib.PubSub.Events
{
    /// <summary>Class representing a pubsub service error event.</summary>
    public class OnPubSubServiceErrorArgs
    {
        /// <summary>Property representing exception.</summary>
        public Exception Exception;
    }
}
