namespace TwitchLib.Events.PubSub
{
    #region using directives
    using System;
    #endregion
    /// <summary>Class representing a pubsub service error event.</summary>
    public class OnPubSubServiceErrorArgs
    {
        /// <summary>Property representing exception.</summary>
        public Exception Exception;
    }
}
