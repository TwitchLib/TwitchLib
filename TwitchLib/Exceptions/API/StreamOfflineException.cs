using System;

namespace TwitchLib.Exceptions.API
{
    /// <inheritdoc />
    /// <summary>Exception representing an attempt to fetch stream data on a stream that is offline.</summary>
    public class StreamOfflineException : Exception
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public StreamOfflineException(string apiData) : base(apiData) { }
    }
}
