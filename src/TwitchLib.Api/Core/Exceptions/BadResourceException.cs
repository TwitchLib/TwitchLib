using System;

namespace TwitchLib.Api.Core.Exceptions
{
    /// <inheritdoc />
    /// <summary>Exception representing an invalid resource</summary>
    public class BadResourceException : Exception
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public BadResourceException(string apiData)
            : base(apiData)
        {
        }
    }
}