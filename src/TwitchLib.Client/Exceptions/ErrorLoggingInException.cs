using System;

namespace TwitchLib.Client.Exceptions
{
    /// <summary>
    /// Exception representing credentials provided for logging in were bad.
    /// Implements the <see cref="System.Exception" />
    /// </summary>
    /// <seealso cref="System.Exception" />
    /// <inheritdoc />
    public class ErrorLoggingInException : Exception
    {
        /// <summary>
        /// Exception representing username associated with bad login.
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; protected set; }

        /// <summary>
        /// Exception construtor.
        /// </summary>
        /// <param name="ircData">The irc data.</param>
        /// <param name="twitchUsername">The twitch username.</param>
        /// <inheritdoc />
        public ErrorLoggingInException(string ircData, string twitchUsername)
            : base(ircData)
        {
            Username = twitchUsername;
        }
    }
}