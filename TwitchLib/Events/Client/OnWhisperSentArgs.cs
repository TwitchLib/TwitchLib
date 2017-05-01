namespace TwitchLib.Events.Client
{
    #region using directives
    using System;
    #endregion
    /// <summary>Args representing whisper sent event.</summary>
    public class OnWhisperSentArgs : EventArgs
    {
        /// <summary>Property representing username of bot.</summary>
        public string Username;
        /// <summary>Property representing receiver of the whisper.</summary>
        public string Receiver;
        /// <summary>Property representing sent message contents.</summary>
        public string Message;
    }
}
