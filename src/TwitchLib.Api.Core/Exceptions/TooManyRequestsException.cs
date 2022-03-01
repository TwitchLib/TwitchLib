using System;

namespace TwitchLib.Api.Core.Exceptions
{
    /// <inheritdoc />
    /// <summary>Exception representing a 429 Http Statuscode</summary>
    public sealed class TooManyRequestsException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        /// Constructor that allows a reset time to be added
        /// </summary>
        /// <param name="data">This is the exception message as a string</param>
        /// <param name="resetTime">This is the reset time from twitch as a linux timestamp</param>
        public TooManyRequestsException(string data, string resetTime)
            : base(data)
        {
            if (double.TryParse(resetTime, out var time))
            {
                Data.Add("Ratelimit-Reset", time);
            }
        }
    }
}