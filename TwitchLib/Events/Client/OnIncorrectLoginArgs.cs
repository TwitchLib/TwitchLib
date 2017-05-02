namespace TwitchLib.Events.Client
{
    #region using directives
    using System;
    using Exceptions.Client;
    #endregion
    /// <summary>Args representing an incorrect login event.</summary>
    public class OnIncorrectLoginArgs : EventArgs
    {
        /// <summary>Property representing exception object.</summary>
        public ErrorLoggingInException Exception;
    }
}
