using System;

namespace TwitchLib.Exceptions.Client
{
    /// <inheritdoc />
    /// <summary>Exception representing bad information being provided to function/method.</summary>
    public class InvalidParameterException : Exception
    {
        /// <summary>Username that had the exception.</summary>
        public string Username { get; protected set; }

        /// <inheritdoc />
        /// <summary>Exception construtor.</summary>
        public InvalidParameterException(string reasoning, string twitchUsername)
            : base(reasoning)
        {
            Username = twitchUsername;
        }
    }
}
