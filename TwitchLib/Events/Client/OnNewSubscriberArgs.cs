namespace TwitchLib.Events.Client
{
    #region using directives
    using System;
    using Models.Client;
    #endregion
    /// <summary>Args representing new subscriber event.</summary>
    public class OnNewSubscriberArgs : EventArgs
    {
        /// <summary>Property representing subscriber object.</summary>
        public Subscriber Subscriber;
        /// <summary>Property representing channel bot is connected to.</summary>
        public string Channel;
    }
}
