using System;
using TwitchLib.Client.Models.Client;

namespace TwitchLib.Client.Events.Client
{
    /// <inheritdoc />
    /// <summary>Args representing resubscriber event.</summary>
    public class OnReSubscriberArgs : EventArgs
    {
        /// <summary>Property representing resubscriber object.</summary>
        public ReSubscriber ReSubscriber;
    }
}
