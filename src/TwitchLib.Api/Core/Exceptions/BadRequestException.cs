using System;

namespace TwitchLib.Api.Core.Exceptions
{
    /// <inheritdoc />
    /// <summary>Exception representing a request that doesn't have a clientid attached.</summary>
    public class BadRequestException : Exception
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public BadRequestException(string apiData)
            : base(apiData)
        {
        }
    }
}
