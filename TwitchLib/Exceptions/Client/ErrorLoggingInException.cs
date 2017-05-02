namespace TwitchLib.Exceptions.Client
{
    #region using directives
    using System;
    #endregion
    /// <summary>Exception representing credentials provided for logging in were bad.</summary>
    public class ErrorLoggingInException : Exception
    {
        /// <summary>Exception representing username associated with bad login.</summary>
        public string Username { get; protected set; }
        /// <summary>Exception construtor.</summary>
        public ErrorLoggingInException(string ircData, string twitchUsername)
            : base(ircData)
        {
            Username = twitchUsername;
        }
    }
}