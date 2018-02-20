using System;

namespace TwitchLib.Exceptions.Client
{
    /// <inheritdoc />
    /// <summary>Exception representing credentials provided for logging in were bad.</summary>
    public class ErrorLoggingInException : Exception
    {
        /// <summary>Exception representing username associated with bad login.</summary>
        public string Username { get; protected set; }

        /// <inheritdoc />
        /// <summary>Exception construtor.</summary>
        public ErrorLoggingInException(string ircData, string twitchUsername)
            : base(ircData)
        {
            Username = twitchUsername;
        }
    }
}