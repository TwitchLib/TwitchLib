namespace TwitchLib.Events.Client
{
    #region using directives
    using System;
    using Models.Client;
    #endregion
    /// <summary>Args representing message received event.</summary>
    public class OnMessageReceivedArgs : EventArgs
    {
        /// <summary>Property representing received chat message.</summary>
        public ChatMessage ChatMessage;
    }
}
