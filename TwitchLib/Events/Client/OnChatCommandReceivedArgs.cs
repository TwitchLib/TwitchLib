namespace TwitchLib.Events.Client
{
    #region using directives
    using System;
    using Models.Client;
    #endregion
    /// <summary>Args representing chat command received event.</summary>
    public class OnChatCommandReceivedArgs : EventArgs
    {
        /// Property representing received command.
        public ChatCommand Command;
    }
}
