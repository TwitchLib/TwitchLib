using System;

namespace TwitchLib.Api.Core.Exceptions
{
    /// <inheritdoc />
    /// <summary>Exception representing a detection that the OAuth token expired</summary>
    public class TokenExpiredException : Exception
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public TokenExpiredException(string data)
            : base(data)
        {
        }
    }
}