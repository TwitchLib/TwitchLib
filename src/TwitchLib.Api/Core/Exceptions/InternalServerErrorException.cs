using System;

namespace TwitchLib.Api.Core.Exceptions
{
    /// <inheritdoc />
    /// <summary>Exception representing a 500 Http Statuscode</summary>
    public class InternalServerErrorException : Exception
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public InternalServerErrorException(string data)
            : base(data)
        {
        }
    }
}