namespace TwitchLib.Events.Client
{
    #region using directives
    using System;
    using Models.Client;
    #endregion
    /// <summary></summary>
    public class OnWhisperReceivedArgs : EventArgs
    {
        /// <summary></summary>
        public WhisperMessage WhisperMessage;
    }
}
