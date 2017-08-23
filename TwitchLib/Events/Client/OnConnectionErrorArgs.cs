namespace TwitchLib.Events.Client
{
    #region using directives
    using System;
    using Models.Client;
    #endregion
    /// <summary>Args representing client connection error event.</summary>
    public class OnConnectionErrorArgs : EventArgs
    {
        /// <summary></summary>
        public ErrorEvent Error;
        /// <summary>Username of the bot that suffered connection error.</summary>
        public string BotUsername;
    }
}
