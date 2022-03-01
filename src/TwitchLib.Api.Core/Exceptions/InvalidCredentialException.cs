using System;

namespace TwitchLib.Api.Core.Exceptions
{
    /// <inheritdoc />
    /// <summary>Exception representing a detection that sent credentials were invalid.</summary>
    public class InvalidCredentialException : Exception
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public InvalidCredentialException(string data)
            : base(data)
        {
        }
    }
}
