namespace TwitchLib.Events.Client
{
    #region using directives
    using System;
    using Models.Client;
    #endregion
    /// <summary>Args representing resubscriber event.</summary>
    public class OnReSubscriberArgs : EventArgs
    {
        /// <summary>Property representing resubscriber object.</summary>
        public Subscriber ReSubscriber;
    }
}
