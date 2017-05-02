namespace TwitchLib.Events.Client
{
    #region using directives
    using System;
    #endregion
    /// <summary>Args representing client disconnect event.</summary>
    public class OnDisconnectedArgs : EventArgs
    {
        /// <summary>Username of the bot that was disconnected.</summary>
        public string Username;
    }
}
