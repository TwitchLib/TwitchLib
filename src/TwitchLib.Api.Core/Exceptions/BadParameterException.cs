using System;

namespace TwitchLib.Api.Core.Exceptions
{
    /// <inheritdoc />
    /// <summary>Exception representing an invalid resource</summary>
    public class BadParameterException : Exception
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public BadParameterException(string badParamData)
            : base(badParamData)
        {
        }
    }
}
