using System;

namespace TwitchLib.Exceptions
{
    /// <summary>Exception representing credentials provided for logging in were bad.</summary>
    public class ErrorLoggingInException : Exception
    {
        private string _twitchUsername;
        /// <summary>Exception representing username associated with bad login.</summary>
        public string Username => _twitchUsername;
        /// <summary>Exception construtor.</summary>
        public ErrorLoggingInException(string ircData, string twitchUsername)
            : base(ircData)
        {
            _twitchUsername = twitchUsername;
        }
    }
}