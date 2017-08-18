namespace TwitchLib.Events.Client
{
    #region using directives
    using System;
    #endregion
    /// <summary>Args representing on connected event.</summary>
    public class OnConnectedArgs : EventArgs
    {
        /// <summary>Property representing bot username.</summary>
        public string BotUsername;
        /// <summary>Property representing connected channel.</summary>
        public string AutoJoinChannel;
    }
}
